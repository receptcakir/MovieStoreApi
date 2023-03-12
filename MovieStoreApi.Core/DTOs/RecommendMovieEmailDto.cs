using MovieStoreApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Core.DTOs
{
    public class RecommendMovieEmailDto
    {
        public Movie Movie { get; set; }
        public string? Email { get; set; }
    }
}
