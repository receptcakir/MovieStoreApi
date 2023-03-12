using MovieStoreApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Core.DTOs
{
    public class MovieCommentAddDto
    {
        public int MovieId { get; set; }
        public string Comment { get; set; }
        public decimal Rating { get; set; }
    }
}
