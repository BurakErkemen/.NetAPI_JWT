using CoreLayer.DTOs;
using CoreLayer.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto userAppDto)
        {
            var result = await userService.CreateUserAsync(userAppDto);
            return CreateActionResultInstance(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return CreateActionResultInstance(await userService.GetUserByNameAsync(HttpContext.User.Identity!.Name!));
        }

        [HttpPost("CreateUserRole/{userName}")]
        public async Task<IActionResult> CreateUserRole(string userName)
        {
            var result = await userService.CreateUserRole(userName);
            return CreateActionResultInstance(result);
        }

    }
}
