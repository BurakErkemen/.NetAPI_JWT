﻿using CoreLayer.DTOs;
using CoreLayer.Models;
using CoreLayer.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.DTO;

namespace ServiceLayer.Services
{
    public class UserService(UserManager<UserAppModel> userManager,RoleManager<IdentityRole> roleManager) : IUserService
    {
        public async Task<Response<UserModelDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new UserAppModel
            {
                UserName = createUserDto.UserName,
                Email = createUserDto.Email
            };

            var result = await userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return Response<UserModelDto>.Fail(new ErrorDto(errors, true),400);
            }

            return Response<UserModelDto>.Success(ObjectMapper.Mapper.Map<UserModelDto>(user), 200);
        }

        public async Task<Response<NoDataDTO>> CreateUserRole(string userName)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
           

            var user = await userManager.FindByNameAsync(userName);

            if (user is null)
            {
                return Response<NoDataDTO>.Fail("User not found", 404, true);
            }

            await userManager.AddToRoleAsync(user, "Admin");

            return Response<NoDataDTO>.Success(201);
        }

        public async Task<Response<UserModelDto>> GetUserByNameAsync(string userName)
        {
            var anyUser = await userManager.FindByNameAsync(userName);

            if (anyUser is null)
            {
                return Response<UserModelDto>.Fail("User Name not found", 404, true);
            }

            return Response<UserModelDto>.Success(ObjectMapper.Mapper.Map<UserModelDto>(anyUser), 200);
        }


    }
}