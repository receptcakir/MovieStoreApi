using MovieStoreApi.Core.DTOs;
using MovieStoreApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(AppUser userApp);
    }
}
