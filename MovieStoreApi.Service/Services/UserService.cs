using AutoMapper;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Identity;
using Mikro.Task.Services.Application.Helpers;
using MovieStoreApi.Core.DTOs.User;
using MovieStoreApi.Core.Models;
using MovieStoreApi.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDto> CreateUserAsync(UserRegisterDto userRegisterDto)
        {
            var userExists = await _userManager.FindByNameAsync(userRegisterDto.Email);
            if (userExists != null) throw new CustomException("User already exists");

            var user = new AppUser {
                Id = Guid.NewGuid().ToString(),
                Email = userRegisterDto.Email,
                UserName = userRegisterDto.Username           
            };

            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);

            if (!result.Succeeded)
            {
                throw new CustomException();
            }

            return new UserDto() { Email = user.Email, UserName = user.UserName };
        }

        public async Task<UserDto> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null) throw new NotFoundException("UserName not found");

            return new UserDto() { Email = user.Email };
        }

    }
}
