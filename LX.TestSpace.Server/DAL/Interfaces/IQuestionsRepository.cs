using LX.TestSpace.Server.DAL.Entities;

namespace LX.TestSpace.Server.DAL.Interfaces
{
    public interface IQuestionsRepository : IRepository<Question>
    {
        Task DeleteManyAsync(IEnumerable<Question> entities);

        Task<Question> CreateAndReturnAsync(Question question);
    }
}
