using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Core.DTOs
{
    public class TheMovieCollection
    {
        public TheMovieDates dates { get; set; }
        public int page { get; set; }
        public List<TheMovieModel> results { get; set; }
    }

    public class TheMovieDates
    {
        public string maximum { get; set; }
        public string minimum { get; set; }
    }

    public class TheMovieModel
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public List<int> genre_ids { get; set; }
        public int id { get; set; }
        public int dbid { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
    }
}
