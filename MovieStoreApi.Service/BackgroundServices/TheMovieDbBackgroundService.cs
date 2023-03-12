using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieStoreApi.Core.DTOs;
using MovieStoreApi.Core.Repositories;
using MovieStoreApi.Core.Services;
using MovieStoreApi.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Service.BackgroundServices
{
    public class TheMovieDbBackgroundService : HostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly HttpClient _httpClient;
        private readonly IMovieRepository _movieRepository;
        ITheMovieService _theMovieService;

        public TheMovieDbBackgroundService(IConfiguration configuration, IServiceScopeFactory serviceScopeFactory, HttpClient httpClient, ITheMovieService movieService, IMovieRepository movieRepository)
        {
            _configuration = configuration;
            _serviceScopeFactory = serviceScopeFactory;
            _httpClient = httpClient;
            _theMovieService = movieService;
            _movieRepository = movieRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
                string Api_Url = $"https://api.themoviedb.org/3/movie/now_playing?api_key={_configuration["TheMovieDbApiKey"]}";


                while (!stoppingToken.IsCancellationRequested)
                {
                    var _db = _movieRepository.GetAll();

                    var response = await _httpClient.GetAsync(Api_Url, stoppingToken);
                    if (response.IsSuccessStatusCode)
                    {
                        var theMovieModel = await response.Content.ReadFromJsonAsync<TheMovieCollection>();
                        var movies = theMovieModel.results.Where(x => !_db.Select(p => p.the_movie_id).Contains(x.id)).ToList();
                        if (movies.Count > 0)
                        {
                            await _theMovieService.AddRangeAsync(movies);
                        }

                    }
                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                }
           
        }
    }
}
