using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.Repository;
using MovieStoreApi.Service.Mapping;
using MovieStoreApi.WebAPI.Modules;
using System.Reflection;
using FluentValidation.AspNetCore;
using Autofac;
using MovieStoreApi.Service.Validations;
using Autofac.Core;
using MassTransit;
using MovieStoreApi.Service.BackgroundServices;
using Microsoft.Extensions.Options;
using MovieStoreApi.Service.RedisServices;
using MovieStoreApi.Core.Models;
using Microsoft.AspNetCore.Identity;
using MovieStoreApi.Core.Services;
using MovieStoreApi.Service.Services;
using MovieStoreApi.Core.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using MovieStoreApi.Service.MailServices;
using MovieStoreApi.Service.Consumers;
using MovieStoreApi.Core.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SendEmailConsumer>();

    // Default Port : 5672
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint("sendemailqueue", e => {e.ConfigureConsumer<SendEmailConsumer>(context);});
    });
});
builder.Services.AddMassTransitHostedService();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IMailService, MailService>();

// Add services to the container.
builder.Services.AddControllers()
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UserRegisterDtoValidator>())
    .AddNewtonsoftJson(x =>x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;


builder.Services.AddSingleton<IHostedService, TheMovieDbBackgroundService>();

builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddHttpClient();

builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConStr")));
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequiredLength = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();


builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
    {
        var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
        opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience[0],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),

            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));

builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));
builder.Services.AddSingleton<RedisService>(sp =>
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
    var redis = new RedisService(redisSettings.Host, redisSettings.Port);
    redis.Connect();
    return redis;
});

//builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
//{
//    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
