using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStoreApi.Core.DTOs.User;
using MovieStoreApi.Core.DTOs;
using MovieStoreApi.Core.Services;
using MovieStoreApi.Service.Services;

namespace MovieStoreApi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public AuthController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UserRegister(UserRegisterDto userRegisterDto)
        {
            return Ok(await _userService.CreateUserAsync(userRegisterDto));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _authenticationService.CreateTokenAsync(loginDto);

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.CreateTokenByRefreshToken(refreshTokenDto.Token);

            return Ok(result);
        }

    }
}
