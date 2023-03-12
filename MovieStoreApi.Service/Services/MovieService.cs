using AutoMapper;
using MovieStoreApi.Core.DTOs;
using MovieStoreApi.Core.Models;
using MovieStoreApi.Core.Repositories;
using MovieStoreApi.Core.Services;
using MovieStoreApi.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MovieStoreApi.Service.RedisServices;
using MassTransit;
using Mikro.Task.Services.Application.Helpers;
using Microsoft.AspNet.Identity;

namespace MovieStoreApi.Service.Services
{
    public class MovieService : ServiceGeneric<Movie, MovieListDto>,IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly RedisService _redisService;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public MovieService(IGenericRepository<Movie> repository, IUnitOfWork unitOfWork, IMapper mapper, IMovieRepository movieRepository, RedisService redisService, ISendEndpointProvider sendEndpointProvider) : base(repository, unitOfWork, mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _redisService = redisService;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<bool> AddCommentAsync(MovieCommentAddDto commentDto,string userId)
        {
            var movie = await _movieRepository.GetByIdAsync(commentDto.MovieId);
            if (movie == null)
                throw new NotFoundException("Movie not found");

            var comment = _mapper.Map<MovieComment>(commentDto);
            comment.UserId = Guid.Parse(userId);

            var result = await _movieRepository.AddCommentAsync(comment);
            await _unitOfWork.CommitAsync();
            if (result == true)
                await _redisService.GetDb().KeyDeleteAsync($"movie{commentDto.MovieId}");

            return result;
        }

        public  List<MovieListDto> GetMoviesByPageSizeAsync(int pageIndex, int pageSize)
        {
            const string redisKey = "movielist";
            List<MovieListDto> result;

            //get from redis
            var redisValue = _redisService.GetDb().StringGet(redisKey);
            if (!string.IsNullOrEmpty(redisValue))
                result = JsonSerializer.Deserialize<List<MovieListDto>>(redisValue);
           
            else
            {
                result = _mapper.Map<List<MovieListDto>>(_movieRepository.GetAll());

                //set to redis
                 _redisService.GetDb().StringSet(redisKey, JsonSerializer.Serialize(result));
            }

            return result.ToList();
        }

        public async Task<MovieDto> GetWithCommentAsync(int id,string UserId)
        {
            ////get from redis
            //var redisValue = await _redisService.GetDb().StringGetAsync($"movie{id}");
            //if (!string.IsNullOrEmpty(redisValue))
            //    return JsonSerializer.Deserialize<MovieDto>(redisValue);

            var result = _mapper.Map<MovieDto>(await _movieRepository.GetWithCommentAsync(id,UserId));
            //set to redis
           // await _redisService.GetDb().StringSetAsync($"movie{id}", JsonSerializer.Serialize(result));

            return result;
        }

        public async Task<bool> RecommendMovieAsync(RecommendMovieDto recommendMovieDto)
        {
            var movie = await _movieRepository.GetByIdAsync(recommendMovieDto.MovieId);

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:sendemailqueue"));
            RecommendMovieEmailDto model = new RecommendMovieEmailDto { Email = recommendMovieDto.Email, Movie = movie };

            await sendEndpoint.Send<RecommendMovieEmailDto>(model);

            return true;
        }
    }
}
