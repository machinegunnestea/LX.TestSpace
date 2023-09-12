using LX.TestSpace.Server.BLL.Models.Records;
using LX.TestSpace.Server.DAL.Entities;
using System.Linq.Expressions;

namespace LX.TestSpace.Server.DAL.Interfaces
{
    public interface ITestResultsRepository : IRepository<TestResult>
    {
        Task<TestResult> CreateAndReturn(TestResult testResult);

        Task<IEnumerable<TestResult>> GetAllIncludingTestAsync();

        Task<IEnumerable<TestResult>> FilterByCriterionAsync(Expression<Func<TestResult, bool>> ctiterion);

        Task<IEnumerable<TestResult>> FilterByCriterionIncludingTestAsync(Expression<Func<TestResult, bool>> ctiterion);

        Task<PaginationData<TestResult>> PaginationFilter(int selectedPage, int itemsPerPage);

        Task<PaginationData<TestResult>> PaginationFilterByUserName(int selectedPage, int itemsPerPage, string userName);

        Task<PaginationData<TestResult>> PaginationFilterByUserId(int selectedPage, int itemsPerPage, string userId);

        Task<PaginationData<TestResult>> PaginationFilterByTestId(int selectedPage, int itemsPerPage, int testId);

        Task<PaginationData<Test>> GetTestsFromSnapshot(int selectedPage, int itemsPerPage);
    }
}
