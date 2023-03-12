using MovieStoreApi.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Core.Services
{
    public interface ITheMovieService
    {
        Task<bool> AddRangeAsync(List<TheMovieModel> data);
    }
}
