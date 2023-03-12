using AutoMapper;
using MovieStoreApi.Core.DTOs;
using MovieStoreApi.Core.DTOs.User;
using MovieStoreApi.Core.Models;

namespace MovieStoreApi.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<Movie, MovieListDto>().ReverseMap();
            CreateMap<Movie, TheMovieModel>().ReverseMap();

            
            CreateMap<Movie, MovieWithCommentDto>().ReverseMap();
            CreateMap<MovieComment, MovieCommentDto>().ReverseMap();
            CreateMap<MovieComment, MovieCommentAddDto>().ReverseMap();
            CreateMap<AppUser, UserDto>().ReverseMap();





        }
    }
}
