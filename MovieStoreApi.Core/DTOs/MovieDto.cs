using MovieStoreApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Core.DTOs
{
    public class MovieDto : MovieListDto
    {
        public List<MovieComment> Comments { get; set; }

    }
}
