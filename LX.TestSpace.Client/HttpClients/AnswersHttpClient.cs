using LX.TestSpace.Client.Entities;
using LX.TestSpace.Client.HttpRepository.Interfaces;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace LX.TestSpace.Client.HttpRepository
{
    public class AnswersHttpClient : IAnswersHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory httpClientFactory;

        public AnswersHttpClient(IHttpClientFactory httpClientFactory)
        {
            
            this.httpClientFactory = httpClientFactory;
            _httpClient = httpClientFactory.CreateClient("WebAPI");
        }

        public async Task<Answer> Create(Answer answer)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/Answers", answer);
            var createdAnswer = await response.Content.ReadFromJsonAsync<Answer>();
            return createdAnswer;
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Answers/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Answer>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/Answers");
            response.EnsureSuccessStatusCode();
            var answers = await response.Content.ReadFromJsonAsync<List<Answer>>();
            return answers;
        }

        public async Task<Answer> Get(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Answers/{id}");
            response.EnsureSuccessStatusCode();
            var answer = await response.Content.ReadFromJsonAsync<Answer>();
            return answer;
        }

        public async Task<bool> Update(Answer answer)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/Answers/{answer.Id}", answer);
            return response.IsSuccessStatusCode;
        }
    }
}