using MovieStoreApi.Core.DTOs;
using MovieStoreApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Core.Repositories
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<List<Movie>> GetMoviesByPageSizeAsync(int pageIndex, int pageSize);
        Task<Movie> GetWithCommentAsync(int id,string UserId);
        Task<bool> AddCommentAsync(MovieComment comment);
    }
}
