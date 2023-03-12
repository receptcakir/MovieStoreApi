using MovieStoreApi.Core.DTOs;
using MovieStoreApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Core.Services
{
    public interface IMovieService : IServiceGeneric<Movie,MovieListDto>
    {
        List<MovieListDto> GetMoviesByPageSizeAsync(int pageIndex, int pageSize);
        Task<MovieDto> GetWithCommentAsync(int id,string UserId);
        Task<bool> AddCommentAsync(MovieCommentAddDto commentDto,string userId);
        Task<bool> RecommendMovieAsync(RecommendMovieDto recommendMovieDto);
    }
}
