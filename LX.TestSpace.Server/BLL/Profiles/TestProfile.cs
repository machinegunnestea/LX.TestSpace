using AutoMapper;
using LX.TestSpace.Server.BLL.Models;
using LX.TestSpace.Server.DAL.Entities;

namespace LX.TestSpace.Server.BLL.Profiles
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<Test, TestDTO>().ReverseMap();
        }
    }
}
