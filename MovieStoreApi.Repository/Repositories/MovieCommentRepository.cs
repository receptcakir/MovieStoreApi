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
    public class MovieCommentRepository : GenericRepository<MovieComment>, IMovieCommentRepository
    {
        public MovieCommentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
