using CoreLayer.DTOs;
using SharedLibrary.DTO;

namespace CoreLayer.UnitOfWork
{
    public interface IUserService
    {
        Task<Response<UserModelDto>> CreateUserAsync(CreateUserDto createUserDto);

        Task<Response<UserModelDto>> GetUserByNameAsync(string userName);

        Task<Response<NoDataDTO>> CreateUserRole(string userName);

    }
}