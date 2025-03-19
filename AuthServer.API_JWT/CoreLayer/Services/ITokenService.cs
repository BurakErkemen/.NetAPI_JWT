using CoreLayer.Configuration;
using CoreLayer.DTOs;
using CoreLayer.Models;

namespace CoreLayer.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(UserAppModel userAppModel);
        ClientTokenDto ClientTokenDto(Client client);
    }
}
