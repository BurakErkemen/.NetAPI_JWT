using CoreLayer.DTOs;
using SharedLibrary.DTO;

namespace CoreLayer.UnitOfWork
{
    public interface IAuthenticationService
    {
        Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto);
        Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken);
        Task<Response<NoDataDTO>> RevokeRefreshToken(string refreshToken); 
        Task<Response<ClientTokenDto>> CreateTokenByClient(ClientLoginDto clientLoginDto);


    }
}