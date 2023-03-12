using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStoreApi.Core.DTOs;
using MovieStoreApi.Core.Services;
using MovieStoreApi.Service.Services;

namespace MovieStoreApi.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _MovieService;
        public MoviesController(IMovieService MovieService)
        {
            _MovieService = MovieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMoviesByPageSizeAsync(int pageIndex, int pageSize)
        {
            var result = _MovieService.GetMoviesByPageSizeAsync(pageIndex,pageSize);

            return Ok(result);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetWithCommentAsync(int id)
        {
            var result = await _MovieService.GetWithCommentAsync(id,User.Identity.GetUserId());

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RecommendAsync(RecommendMovieDto recommendMovieDto)
        {
            var result = await _MovieService.RecommendMovieAsync(recommendMovieDto);
            return Ok(result ? "The movie recommended." : "Unexpected error");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddCommentAsync(MovieCommentAddDto commentAddDto)
        {
            var result = await _MovieService.AddCommentAsync(commentAddDto, User.Identity.GetUserId());
            return Ok(result ? "The comment Added." : "Unexpected error");
        }
    }
}
