using AutoMapper;
using LX.TestSpace.Server.BLL.Models;
using LX.TestSpace.Server.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace LX.TestSpace.Server.BLL.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<User, UserPersonalDataDto>();
        }
    }
}
