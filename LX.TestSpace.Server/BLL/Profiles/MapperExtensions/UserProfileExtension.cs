using AutoMapper;
using LX.TestSpace.Server.BLL.Models;
using LX.TestSpace.Server.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace LX.TestSpace.Server.BLL.Profiles.MapperExtensions
{
    public static class UserProfileExtension
    {
        public static UserDTO MapUserToDto(this IMapper mapper, UserManager<User> userManager, User user)
        {
            var userDTO = mapper.Map<UserDTO>(user);
            userDTO.UserRoles = userManager.GetRolesAsync(user).Result;
            return userDTO;
        }

        public static IEnumerable<UserDTO> MapUsersToDto(this IMapper mapper, UserManager<User> userManager, IEnumerable<User> users)
        {
            var usersDTO = users
                .Select(user =>
                {
                    var userDTO = mapper.Map<UserDTO>(user);
                    userDTO.UserRoles = userManager.GetRolesAsync(user).Result;
                    return userDTO;
                });
            return usersDTO;
        }
    }
}
