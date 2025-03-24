using CoreLayer.Configuration;
using CoreLayer.DTOs;
using CoreLayer.Models;
using CoreLayer.Repositories;
using CoreLayer.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLibrary.DTO;

namespace ServiceLayer.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _genericRepository;
        private readonly UserManager<UserAppModel> _userManager;


        public AuthenticationServices(IOptions<List<Client>> clients, ITokenService tokenService, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> genericRepository, UserManager<UserAppModel> userManager)
        {
            _clients = clients.Value;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
            _userManager = userManager;
        }


        public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto is null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
                return Response<TokenDto>.Fail("Email or password is wrong", 400, true);

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Response<TokenDto>.Fail("Email or password is wrong", 400, true);

            var token = _tokenService.CreateToken(user);

            var userRefreshToken = await _genericRepository.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (userRefreshToken is null)
            {
                userRefreshToken = new UserRefreshToken
                {
                    UserId = user.Id,
                    Token = token.RefreshToken,
                    Expiration = token.RefreshTokenExpiration
                };

                await _genericRepository.AddAsync(userRefreshToken);
            }
            else
            {
                userRefreshToken.Token = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }



            await _unitOfWork.SaveChangesAsync();

            return Response<TokenDto>.Success(token, 200);
        }

        public Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);

            if (client == null)
                return Response<ClientTokenDto>.Fail("ClientId or ClientSecret is not found", 404, true);

            var token = _tokenService.CreateTokenByClient(client);

            return Response<ClientTokenDto>.Success(token, 200);
        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var anyRefreshToken = await _genericRepository.Where(x => x.Token == refreshToken).SingleOrDefaultAsync();

            if (anyRefreshToken is null)
                return Response<TokenDto>.Fail("Refresh token not found", 404, true);

            var user = await _userManager.FindByIdAsync(anyRefreshToken.UserId);

            if (user is null)
                return Response<TokenDto>.Fail("User not found", 404, true);

            var token = _tokenService.CreateToken(user);

            anyRefreshToken.Token = token.RefreshToken;
            anyRefreshToken.Expiration = token.RefreshTokenExpiration;

            await _unitOfWork.SaveChangesAsync();

            return Response<TokenDto>.Success(token, 200);
        }

        public async Task<Response<NoDataDTO>> RevokeRefreshToken(string refreshToken)
        {
            var anyRefreshToken = await _genericRepository.Where(x => x.Token == refreshToken).SingleOrDefaultAsync();

            if (anyRefreshToken is null)
                return Response<NoDataDTO>.Fail("Refresh token not found", 404, true);

            _genericRepository.Delete(anyRefreshToken);

            await _unitOfWork.SaveChangesAsync();

            return Response<NoDataDTO>.Success(200);
        }
    }
}