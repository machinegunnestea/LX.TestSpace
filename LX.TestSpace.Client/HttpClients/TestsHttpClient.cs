using System.Net.Http.Json;
using System.Text.Json;
using LX.TestSpace.Client.Entities;
using LX.TestSpace.Client.HttpRepository.Interfaces;

namespace LX.TestSpace.Client.HttpRepository
{
    public class TestsHttpClient : ITestsHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory httpClientFactory;

        public TestsHttpClient(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            _httpClient = httpClientFactory.CreateClient("WebAPI");
        }

        public async Task<Test> Create(Test test)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/Tests", test);
            var createdTest = await response.Content.ReadFromJsonAsync<Test>();
            return createdTest;
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Tests/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Test>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/Tests");
            response.EnsureSuccessStatusCode();
            var tests = await response.Content.ReadFromJsonAsync<List<Test>>();
            return tests;
        }

        public async Task<Test> GetById(int id, bool includeDetails)
        {
            var response = await _httpClient.GetAsync($"/api/Tests/{id}?includeDetails={includeDetails}");
            response.EnsureSuccessStatusCode();
            var test = await response.Content.ReadFromJsonAsync<Test>();
            return test;
        }

        public async Task<Test> Get(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Tests/{id}");
            response.EnsureSuccessStatusCode();
            var test = await response.Content.ReadFromJsonAsync<Test>();
            return test;
        }

        public async Task<bool> Update(Test test)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/Tests/{test.Id}", test);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Test>> GetPublishedTests()
        {
            var response = await _httpClient.GetAsync("/api/Tests/published");
            response.EnsureSuccessStatusCode();
            var publishedTests = await response.Content.ReadFromJsonAsync<List<Test>>();
            return publishedTests;
        }

        public async Task<PaginationModel<Test>> GetTestsFromSnapshots(int selectedPage, int itemsPerPage)
        {
            var response = await _httpClient.GetAsync($"/api/TestResults/TestSnapshots?selectedPage={selectedPage}&itemsPerPage={itemsPerPage}");
            response.EnsureSuccessStatusCode();
            var tests = await response.Content.ReadFromJsonAsync<PaginationModel<Test>>();
            return tests;
        }
        public async Task<IEnumerable<Test>> SearchTestByTitle(string searchName)
        {
            var response = await _httpClient.GetAsync($"/api/Tests/ByTestName?searchName={searchName}");
            response.EnsureSuccessStatusCode();
            var tests = await response.Content.ReadFromJsonAsync<IEnumerable<Test>>();
            return tests;
        }

    }
}
