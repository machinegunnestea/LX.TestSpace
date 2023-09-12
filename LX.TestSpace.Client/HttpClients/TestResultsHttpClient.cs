using System;
using System.Net.Http.Json;
using System.Text.Json;
using LX.TestSpace.Client.Entities;
using LX.TestSpace.Client.HttpRepository.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;


namespace LX.TestSpace.Client.HttpRepository
{
    public class TestResultsHttpClient : ITestResultsHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _anonymousHttpClient;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public TestResultsHttpClient(HttpClient httpClient, IHttpClientFactory httpClientFactory, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClientFactory = httpClientFactory;
            _httpClient = httpClientFactory.CreateClient("WebAPI");
            _anonymousHttpClient = httpClientFactory.CreateClient("public");
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<TestResult> Create(TestResult answer)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/TestResults", answer);
            var createdTestResult = await response.Content.ReadFromJsonAsync<TestResult>();
            return createdTestResult;
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/TestResults/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<TestResult>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/TestResults");
            response.EnsureSuccessStatusCode();
            var testResult = await response.Content.ReadFromJsonAsync<List<TestResult>>();
            return testResult;
        }
        public async Task<TestResult> Get(int id)
        {
            var response = await _httpClient.GetAsync($"/api/TestResults/{id}");
            response.EnsureSuccessStatusCode();
            var testResult = await response.Content.ReadFromJsonAsync<TestResult>();
            return testResult;
        }

        public async Task<bool> Update(TestResult testResult)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/answers/{testResult.Id}", testResult);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> GetShareUrl(TestResult testResult)
        {
            var response = await _httpClient.GetStringAsync($"/api/TestResults/shared/encrypt/{testResult.Id}");
            return System.Web.HttpUtility.UrlEncode(response);
        }

        public async Task<TestResult> GetShareTestResult(string url)
        {
            var encodeUrl = System.Web.HttpUtility.UrlEncode(url);
            return await _anonymousHttpClient.GetFromJsonAsync<TestResult>($"/api/TestResults/shared/{encodeUrl}");
        }

        public async Task<TestResult> TestStart(int id)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var userName = authState.User.Identity.Name;
            var response = await _httpClient.GetAsync($"/api/TestResults/start/{id}/{userName}");
            response.EnsureSuccessStatusCode();
            var testResult = await response.Content.ReadFromJsonAsync<TestResult>();
            Console.WriteLine(testResult.Id);
            return testResult;
        }

        public async Task<bool> SaveIntermediateResult(int testResultId, int questionId, QuestionSnapshot questionSnap)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/testresults/intermediate/{testResultId}/{questionId}", questionSnap);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EndTestExecution(int id)
        {
            var response = await _httpClient.GetAsync($"/api/TestResults/end/{id}");
            return response.IsSuccessStatusCode;
        }
        
        public async Task<List<TestResult>> GetTestResultsByUserId(string id)
        {
            var response = await _httpClient.GetAsync($"/api/TestResults/testResults/{id}");
            response.EnsureSuccessStatusCode();
            var testResult = await response.Content.ReadFromJsonAsync<List<TestResult>>();
            return testResult;
        }
        
        public async Task<List<TestResult>> GetTestResultByTestId(int testId)
        {
            var response = await _httpClient.GetAsync($"/api/TestResults/Tests/{testId}");
            response.EnsureSuccessStatusCode();
            var testResult = await response.Content.ReadFromJsonAsync<List<TestResult>>();
            return testResult ?? new List<TestResult>();
        }
        
        public async Task<PaginationModel<TestResult>> GetAll(int selectedPage, int itemsPerPage)
        {
            var response = await _httpClient.GetAsync($"/api/TestResults?selectedPage={selectedPage}&itemsPerPage={itemsPerPage}");
            response.EnsureSuccessStatusCode();
            var testResult = await response.Content.ReadFromJsonAsync<PaginationModel<TestResult>>();
            return testResult ?? new PaginationModel<TestResult>();
        }

        public async Task<PaginationModel<TestResult>> FilterByUserId(int selectedPage, int itemsPerPage, string userId)
        {
            var response = await _httpClient.GetAsync($"/api/TestResults/Users?selectedPage={selectedPage}&itemsPerPage={itemsPerPage}&userId={userId}");
            response.EnsureSuccessStatusCode();
            var testResult = await response.Content.ReadFromJsonAsync<PaginationModel<TestResult>>() ?? new PaginationModel<TestResult>();
            return testResult;
        }

        public async Task<PaginationModel<TestResult>> FilterByTestId(int selectedPage, int itemsPerPage, int testId)
        {
            var response = await _httpClient.GetAsync($"/api/TestResults/Tests?selectedPage={selectedPage}&itemsPerPage={itemsPerPage}&testId={testId}");
            response.EnsureSuccessStatusCode();
            var testResult = await response.Content.ReadFromJsonAsync<PaginationModel<TestResult>>();
            return testResult ?? new PaginationModel<TestResult>();
        }
    }
}
