using System.Linq.Expressions;

using LX.TestSpace.Server.DAL.Entities;

namespace LX.TestSpace.Server.DAL.Interfaces
{
    public interface ITestRepository : IRepository<Test>
    {
        Task<IEnumerable<Test>> FilterByCriterion(Expression<Func<Test, bool>> criterion);

        Task<Test> CreateAndReturn(Test test);
    }
}
