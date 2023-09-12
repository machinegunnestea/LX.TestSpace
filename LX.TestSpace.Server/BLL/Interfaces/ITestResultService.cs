using LX.TestSpace.Server.BLL.Models;
using LX.TestSpace.Server.BLL.Models.PaginationModel;
using LX.TestSpace.Server.DAL.Entities;

namespace LX.TestSpace.Server.BLL.Interfaces
{
    public interface ITestResultService : IEntityService<TestResultDTO>
    {
        Task<TestResultDTO> TestStart(int testid, string userid);

        Task SaveIntermediateResult(QuestionSnapshot userquestion, int questionid, int testresultid);

        Task EndTestExecution(int testresultid);

        Task<string> EncryptTestResultUrl(int testResultId);

        Task<int> DecryptTestResultUrl(string testResultString);

        Task<IEnumerable<TestResultDTO>> GetTestResultByUserId(string userId);

        Task<IEnumerable<TestResultDTO>> GetTestResultByTestId(int testId);

        Task<PaginationModel<TestResultDTO>> GetTestResultPerPage(int selectedPage, int itemsPerPage);

        Task<PaginationModel<TestResultDTO>> GetTestResultByUserNamePerPage(int selectedPage, int itemsPerPage, string userName);

        Task<PaginationModel<TestResultDTO>> GetTestResultByUserIdPerPage(int selectedPage, int itemsPerPage, string userId);

        Task<PaginationModel<TestResultDTO>> GetTestResultByTestIdPerPage(int selectedPage, int itemsPerPage, int testId);

        Task<PaginationModel<TestDTO>> GetTestsFromSnapshots(int selectedPage, int itemsPerPage);
    }
}
