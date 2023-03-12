using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Core.Models
{
    public class MovieComment : BaseEntity
    {
        public Guid UserId { get; set; }
        public int MovieId { get; set; }
        public string Comment { get; set; }
        public decimal Rating { get; set; }
        public AppUser User { get; set; }
        public Movie Movie { get; set; }
    }
}
