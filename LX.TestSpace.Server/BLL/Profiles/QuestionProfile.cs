using AutoMapper;
using LX.TestSpace.Server.BLL.Models;
using LX.TestSpace.Server.DAL.Entities;

namespace LX.TestSpace.Server.BLL.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDTO>()
                .ForMember(dest => dest.IsMultipleAnswers, opt => opt.MapFrom(question => question.Answers.Where(answer => answer.IsCorrect).Count() > 1))
                .ReverseMap();
        }
    }
}
