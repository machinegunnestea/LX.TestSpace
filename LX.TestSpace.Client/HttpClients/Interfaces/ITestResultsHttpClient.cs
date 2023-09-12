using LX.TestSpace.Client.Entities;

namespace LX.TestSpace.Client.HttpRepository.Interfaces
{
    public interface ITestResultsHttpClient : IHttpClient<TestResult>
    {
        Task<TestResult> TestStart(int id);
        Task<bool> SaveIntermediateResult(int testResultId, int questionId, QuestionSnapshot questionSnap);
        Task<bool> EndTestExecution(int id);
        Task<string> GetShareUrl(TestResult testResult);
        Task<TestResult> GetShareTestResult(string url);
        Task<List<TestResult>> GetTestResultsByUserId(string id);
        Task<List<TestResult>> GetTestResultByTestId(int testId);
        Task<PaginationModel<TestResult>> GetAll(int selectedPage, int itemsPerPage);
        Task<PaginationModel<TestResult>> FilterByUserId(int selectedPage, int itemsPerPage, string userId);
        Task<PaginationModel<TestResult>> FilterByTestId(int selectedPage, int itemsPerPage, int testId);
    }
}
