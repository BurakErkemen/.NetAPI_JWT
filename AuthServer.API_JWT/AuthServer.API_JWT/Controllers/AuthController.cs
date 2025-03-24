using CoreLayer.DTOs;
using CoreLayer.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API_JWT.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController(IAuthenticationServices authenticationService) : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto model)
        {
            var result = await authenticationService.CreateTokenAsync(model);

            return CreateActionResultInstance(result);
        }

        [HttpPost]
        public  IActionResult CreateTokenByClient(ClientLoginDto model)
        {
            var result = authenticationService.CreateTokenByClient(model);

            return CreateActionResultInstance(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(string refreshToken)
        {
            var result = await authenticationService.CreateTokenByRefreshToken(refreshToken);
            return CreateActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result =await authenticationService.RevokeRefreshToken(refreshTokenDto.Token);
            return CreateActionResultInstance(result);
        }
    }
}
