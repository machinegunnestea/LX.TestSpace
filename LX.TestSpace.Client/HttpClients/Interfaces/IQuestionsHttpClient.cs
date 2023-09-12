using LX.TestSpace.Client.Entities;
using LX.TestSpace.Client.Entities.Records;

namespace LX.TestSpace.Client.HttpRepository.Interfaces
{
    public interface IQuestionsHttpClient : IHttpClient<Question>
    {
        Task<Question> CreateAndReturn(Question question);
        Task<string> SetImage(QuestionAndImage questionAndImage);
    }
}
