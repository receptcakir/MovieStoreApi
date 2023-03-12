using Microsoft.EntityFrameworkCore;
using MovieStoreApi.Core.Models;
using MovieStoreApi.Core.Repositories;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Repository.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly IMovieCommentRepository _movieCommentRepository;
        public MovieRepository(AppDbContext context, IMovieCommentRepository movieCommentRepository) : base(context)
        {
            _movieCommentRepository = movieCommentRepository;
        }

        public async Task<bool> AddCommentAsync(MovieComment movieComment)
        {
             await _movieCommentRepository.AddAsync(movieComment);
            return true;
        }

        public async Task<List<Movie>> GetMoviesByPageSizeAsync(int pageIndex, int pageSize)
        {
            return await _context.Movies.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
        }


        public async Task<Movie> GetWithCommentAsync(int id,string UserId)
        {
            return await _context.Movies
             .Where(m => m.Id == id )
             .Include(m => m.Comments)
             .Where(a => a.Comments.Any(c => c.UserId == Guid.Parse(UserId)))
             .FirstOrDefaultAsync();
        }
    }
}
