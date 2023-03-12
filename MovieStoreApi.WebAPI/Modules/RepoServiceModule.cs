using Autofac;
using MovieStoreApi.Core.Repositories;
using MovieStoreApi.Core.Services;
using MovieStoreApi.Core.UnitOfWorks;
using MovieStoreApi.Repository;
using MovieStoreApi.Service.Mapping;
using MovieStoreApi.Repository.UnitOfWorks;
using MovieStoreApi.Service.Services;
using Repository.Repositories;
using System.Reflection;
using Module = Autofac.Module;
using Autofac.Core;

namespace MovieStoreApi.WebAPI.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(ServiceGeneric<,>)).As(typeof(IServiceGeneric<,>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            //builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            //builder.RegisterType<UserService>().As<IUserService>();
            //builder.RegisterType<TokenService>().As<ITokenService>();


            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();




        }
    }
}

