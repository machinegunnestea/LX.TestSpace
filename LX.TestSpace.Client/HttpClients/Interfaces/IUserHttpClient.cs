using LX.TestSpace.Client.Entities;

namespace LX.TestSpace.Client.HttpRepository.Interfaces
{
    public interface IUserHttpClient
    {
        Task<User> Create(User item);
        Task<List<User>> GetAll();
        Task<bool> Update(User item);
        Task<PaginationModel<TestResult>> GetTestResultByUserNamePerPage(int selectedPage, int itemsPerPage, string userName);
        Task<User> GetById(string id);
        Task<PaginationModel<UserPersonalData>> GetAllPersonalData(int page, int pageSize);
    }
}
