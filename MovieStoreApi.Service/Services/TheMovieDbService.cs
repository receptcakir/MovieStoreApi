using AutoMapper;
using MovieStoreApi.Core.DTOs;
using MovieStoreApi.Core.Models;
using MovieStoreApi.Core.Repositories;
using MovieStoreApi.Core.Services;
using MovieStoreApi.Core.UnitOfWorks;
using MovieStoreApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Service.Services
{
    public class TheMovieDbService : ITheMovieService
    {
        protected readonly AppDbContext _context;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TheMovieDbService(AppDbContext context)
        {
            _context = context;
        }



        public async Task<bool> AddRangeAsync(List<TheMovieModel> dataDto)
        {
            foreach (var movie in dataDto)
            {
                var dbModel = new Movie
                {
                    the_movie_id = movie.id,
                    adult = movie.adult,
                    backdrop_path = movie.backdrop_path,
                    original_language = movie.original_language,
                    original_title = movie.original_title,
                    overview = movie.overview,
                    popularity = movie.popularity,
                    poster_path = movie.poster_path,
                    release_date = movie.release_date,
                    title = movie.title,
                    video = movie.video,
                    vote_average = movie.vote_average,
                    vote_count = movie.vote_count,
                };
                 _context.Movies.Add(dbModel);

            }
                await _context.SaveChangesAsync();
            return true;

        }
    }
}
