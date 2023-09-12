using LX.TestSpace.Server.BLL.Models;

namespace LX.TestSpace.Server.BLL.Interfaces
{
    public interface ITestService : IEntityService<TestDTO>
    {
        Task<TestDTO> GetByIdAsync(int id, bool includeDetails);

        Task<TestDTO> CreateAndReturnEntityAsync(TestDTO test);

        Task<IEnumerable<TestDTO>> GetPublishedTests();

        Task<IEnumerable<TestDTO>> SearchTestByTitle(string testTitle);
    }
}
