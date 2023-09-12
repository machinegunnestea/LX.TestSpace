using LX.TestSpace.Server.DAL.Entities;

namespace LX.TestSpace.Server.DAL.Interfaces
{
    public interface IAnswersRepository : IRepository<Answer>
    {
        Task DeleteManyAsync(IEnumerable<Answer> entities);
    }
}
