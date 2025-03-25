using CoreLayer.Configuration;
using CoreLayer.DTOs;
using CoreLayer.Models;

namespace CoreLayer.UnitOfWork
{
    public interface ITokenService
    {
        Task<TokenDto> CreateToken(UserAppModel userAppModel);
        ClientTokenDto CreateTokenByClient(Client client);
    }
}
