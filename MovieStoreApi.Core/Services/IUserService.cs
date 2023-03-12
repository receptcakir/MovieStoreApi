using MovieStoreApi.Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Core.Services
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(UserRegisterDto userRegisterDto);

        Task<UserDto> GetUserByNameAsync(string userName);
    }
}
