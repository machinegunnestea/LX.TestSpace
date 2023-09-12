using LX.TestSpace.Client.Entities;
using LX.TestSpace.Client.HttpRepository.Interfaces;
using System.Net.Http.Json;

namespace LX.TestSpace.Client.HttpRepository
{
    public class UserHttpClient: IUserHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _anonymousHttpClient;
        private readonly IHttpClientFactory httpClientFactory;

        public UserHttpClient(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            _httpClient = httpClientFactory.CreateClient("WebAPI");
            _anonymousHttpClient = httpClientFactory.CreateClient("public");
        }

        public async Task<User> Create(User user)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/Users", user);
            var createdUser = await response.Content.ReadFromJsonAsync<User>();
            return createdUser;
        }

        public async Task<List<User>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/Users");
            response.EnsureSuccessStatusCode();
            var users = await response.Content.ReadFromJsonAsync<List<User>>();
            return users;
        }

        public async Task<PaginationModel<UserPersonalData>> GetAllPersonalData(int page, int pageSize)
        {
            var response = await _httpClient.GetAsync($"/api/Users/PersonalData?page={page}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();
            var users = await response.Content.ReadFromJsonAsync<PaginationModel<UserPersonalData>>() ?? new PaginationModel<UserPersonalData>();
            return users;
        }

        public async Task<bool> Update(User user)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/Users/{user.Id}", user);
            return response.IsSuccessStatusCode;
        }
        public async Task<PaginationModel<TestResult>> GetTestResultByUserNamePerPage(int selectedPage, int itemsPerPage, string userName)
        {
            var response = await _httpClient.GetAsync($"/api/TestResults/userTestResults?selectedPage={selectedPage}&itemsPerPage={itemsPerPage}&userName={userName}");
            response.EnsureSuccessStatusCode();
            var testResults = await response.Content.ReadFromJsonAsync<PaginationModel<TestResult>>();
            return testResults;
        }
        public async Task<User> GetById(string id)
        {
            var response = await _anonymousHttpClient.GetAsync($"/api/Users/{id}");
            response.EnsureSuccessStatusCode();
            var user = await response.Content.ReadFromJsonAsync<User>();
            return user;
        }
    }
}
