using LX.TestSpace.Client.Entities;

namespace LX.TestSpace.Client.HttpRepository.Interfaces
{
    public interface ITestsHttpClient : IHttpClient<Test>
    {
        Task<Test> GetById(int id, bool includeDdetails);
        Task<List<Test>> GetPublishedTests();
        Task<PaginationModel<Test>> GetTestsFromSnapshots(int selectedPage, int itemsPerPage);
        Task<IEnumerable<Test>> SearchTestByTitle(string searchName);
    }
}
