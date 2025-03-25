using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiniApp1.API.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStock()
        {
            var usreName = HttpContext.User.Identity!.Name;
            var userId = User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier!);

            return Ok($"Stock İslemleri  UserName = {usreName} , UserId = {userId.Value}");
        }
    }
}
