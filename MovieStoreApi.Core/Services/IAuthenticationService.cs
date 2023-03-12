using MovieStoreApi.Core.DTOs;
using MovieStoreApi.Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Core.Services
{
    public interface IAuthenticationService
    {
        Task<TokenDto> CreateTokenAsync(LoginDto loginDto);

        Task<TokenDto> CreateTokenByRefreshToken(string refreshToken);
    }
}
