using LX.TestSpace.Server.BLL.Models;

namespace LX.TestSpace.Server.BLL.Interfaces
{
    public interface IQuestionService : IEntityService<QuestionDTO>
    {
        Task<QuestionDTO> CreateAndReturnAsync(QuestionDTO entity);

        Task<string> AttachImageToQuestionAsync(int id, Stream imageStream, string fileExtension);
    }
}
