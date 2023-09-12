using System.Net.Http.Json;
using System.Text.Json;
using LX.TestSpace.Client.Entities;
using LX.TestSpace.Client.Entities.Records;
using LX.TestSpace.Client.HttpRepository.Interfaces;

namespace LX.TestSpace.Client.HttpRepository
{
    public class QuestionsHttpClient : IQuestionsHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory httpClientFactory;

        public QuestionsHttpClient(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            _httpClient = httpClientFactory.CreateClient("WebAPI");
        }

        public async Task<Question> Create(Question question)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/Questions", question);
            var createdQuestion = await response.Content.ReadFromJsonAsync<Question>();
            return createdQuestion;
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Questions/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Question>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/Questions");
            response.EnsureSuccessStatusCode();
            var questions = await response.Content.ReadFromJsonAsync<List<Question>>();
            return questions;
        }

        public async Task<Question> Get(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Questions/{id}");
            response.EnsureSuccessStatusCode();
            var question = await response.Content.ReadFromJsonAsync<Question>();
            return question;
        }

        public async Task<bool> Update(Question question)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/Questions/{question.Id}", question);
            return response.IsSuccessStatusCode;
        }

        public async Task<Question> CreateAndReturn(Question question)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/Questions", question);
            var createdQuestion = await response.Content.ReadFromJsonAsync<Question>();
            return createdQuestion;
        }

        public async Task<string> SetImage(QuestionAndImage questionAndImage)
        {
            var content = new MultipartFormDataContent();
            var stream = new MemoryStream(questionAndImage.BufferedImage);
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            content.Add(content: fileContent, name: "\"image\"", fileName: "image.jpg");
            var response = await _httpClient.PostAsync($"/api/Questions/{questionAndImage.Question.Id}/image", content);
            var imagePath = await response.Content.ReadAsStringAsync();
            return imagePath;
        }
    }
}
