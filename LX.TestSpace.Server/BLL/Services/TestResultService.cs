using AutoMapper;
using LX.TestSpace.Server.BLL.Interfaces;
using LX.TestSpace.Server.BLL.Models;
using LX.TestSpace.Server.BLL.Models.PaginationModel;
using LX.TestSpace.Server.DAL.Entities;
using LX.TestSpace.Server.DAL.Interfaces;
using LX.TestSpace.Server.DAL.Repositories;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using System.Text;
namespace LX.TestSpace.Server.BLL.Services
{
    public class TestResultService : ITestResultService
    {
        private readonly ITestResultsRepository testResultsRepository;
        private readonly ITestRepository testRepository;
        private readonly IQuestionsRepository questionRepository;
        private readonly IMapper mapper;
        private DESCryptoServiceProvider des;

        public TestResultService(DESCryptoServiceProvider des, ITestResultsRepository testResultsRepository, IQuestionsRepository questionRepository, ITestRepository testRepository, IMapper mapper)
        {
            this.des = des;
            this.testRepository = testRepository;
            this.questionRepository = questionRepository;
            this.testResultsRepository = testResultsRepository;
            this.mapper = mapper;
        }

        public async Task CreateAsync(TestResultDTO entity)
        {
            await testResultsRepository.CreateAsync(mapper.Map<TestResult>(entity));
        }

        public async Task DeleteAsync(TestResultDTO entity)
        {
            await testResultsRepository.DeleteAsync(mapper.Map<TestResult>(entity));
        }

        public async Task<IEnumerable<TestResultDTO>> GetAsync()
        {
            return mapper.Map<IEnumerable<TestResultDTO>>(await testResultsRepository.GetAllAsync());
        }

        public async Task UpdateAsync(TestResultDTO entity)
        {
            await this.testResultsRepository.UpdateAsync(this.mapper.Map<TestResult>(entity));
        }
        
        public async Task<TestResultDTO> GetByIdAsync(int id)
        {
            return mapper.Map<TestResultDTO>(await testResultsRepository.GetByIdAsync(id));
        }

        public async Task SaveIntermediateResult(QuestionSnapshot userQuestion, int questionId, int testResultId)
        {
            var question = (await this.questionRepository.GetAllAsync())
                .FirstOrDefault(x => x.Id == questionId);

            var testResult = (await this.testResultsRepository.GetAllAsync())
                .FirstOrDefault(x => x.Id == testResultId);

            var correctAnswers = question.Answers
                .Where(answer => answer.IsCorrect);

            var correctSelectedAnswers = userQuestion.UserAnswersSnapshots
                .Where(useranswer => correctAnswers
                .Any(correctanswer => correctanswer.Id == useranswer.AnswerId && useranswer.IsSelected));

            userQuestion.QuestionResult = (double)correctSelectedAnswers.Count() / correctAnswers.Count();

            var questionAnswersList = question.Answers.ToArray();

            int answersCount = questionAnswersList.Count();

            for (int i = 0; i < answersCount; i++)
            {
                userQuestion.UserAnswersSnapshots[i].IsCorrect = questionAnswersList[i].IsCorrect;
            }

            testResult.QuestionSnapshots.Add(userQuestion);

            await this.testResultsRepository.UpdateAsync(testResult);
        }

        public async Task<TestResultDTO> TestStart(int testId, string userId)
        {
            //TODO: add GetById, Create -> { id}
            var test = mapper.Map<TestDTO>((await this.testRepository.GetAllAsync())
                .FirstOrDefault(x => x.Id == testId));

            var testResult = new TestResult
            {
                UserId = userId,
                TestId = testId,
                StartedAt = DateTime.Now,
                TestSnapshot = new TestSnapshot(test.Name, test.Description, test.Duration, test.CreationDate, test.UserNameCreate, test.Id),
                TestScore = 0,
            };

            var addedTestResult = await this.testResultsRepository.CreateAndReturn(testResult);
            return this.mapper.Map<TestResultDTO>(addedTestResult);
        }

        public async Task EndTestExecution(int testResultId)
        {
            var testResult = (await this.testResultsRepository.GetAllAsync())
                .FirstOrDefault(x => x.Id == testResultId);
            testResult.FinishedAt = DateTime.Now;
            testResult.TestScore = await this.TestScoreCalculation(testResult.Id);
            await this.testResultsRepository.UpdateAsync(testResult);
        }

        private async Task<double> TestScoreCalculation(int testResultId)
        {
            var testResult = (await this.testResultsRepository.GetAllAsync())
                .FirstOrDefault(x => x.Id == testResultId);
            double result = 0;
            testResult.QuestionSnapshots.ForEach(x =>
            {
                result += x.QuestionResult;
            });
            return Math.Round((double)result * 100 / testResult.QuestionSnapshots.Count(),2);
        }

        //public async Task<string> EncryptTestResultUrl(int testResultId)
        //{

        //    string testResultIdString = Convert.ToString(testResultId);
        //    byte[] inputByteArray = Encoding.UTF8.GetBytes(testResultIdString);
        //    byte[] rgbIV = { 0x21, 0x43, 0x56, 0x87, 0x10, 0xfd, 0xea, 0x1c };
        //    byte[] key = { };
        //    key = System.Text.Encoding.UTF8.GetBytes("A0D1nX0Q");

        //    MemoryStream ms = new MemoryStream();
        //    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        //    cs.Write(inputByteArray, 0, inputByteArray.Length);
        //    cs.FlushFinalBlock();
        //    return Convert.ToBase64String(ms.ToArray());
        //}

        //public async Task<int> DecryptTestResultUrl(string testResultUrl)
        //{

        //    byte[] inputByteArray = new byte[testResultUrl.Length + 1];
        //    byte[] rgbIV = { 0x21, 0x43, 0x56, 0x87, 0x10, 0xfd, 0xea, 0x1c };
        //    byte[] key = { };

        //    key = Encoding.UTF8.GetBytes("A0D1nX0Q");

        //    inputByteArray = Convert.FromBase64String(testResultUrl);
        //    MemoryStream ms = new MemoryStream();
        //    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        //    cs.Write(inputByteArray, 0, inputByteArray.Length);
        //    cs.FlushFinalBlock();
        //    System.Text.Encoding encoding = System.Text.Encoding.UTF8;
        //    return Convert.ToInt32(encoding.GetString(ms.ToArray()));
        //}
        public async Task<string> EncryptTestResultUrl(int testResultId)
        {
            using var aes = Aes.Create();
            aes.Key = new byte[] { 0x10, 0x01, 0x5f, 0x3a, 0x89, 0x7d, 0x6f, 0x04, 0x9c, 0xb6, 0x32, 0x45, 0x78, 0x4d, 0x2e, 0x1f };
            aes.IV = new byte[] { 0x21, 0x43, 0x56, 0x87, 0x10, 0xfd, 0xea, 0x1c, 0x9b, 0x2c, 0xae, 0x4d, 0x5f, 0x6a, 0x7b, 0x8c };

            var inputByteArray = Encoding.UTF8.GetBytes(testResultId.ToString());
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            await cs.WriteAsync(inputByteArray, 0, inputByteArray.Length);
            await cs.FlushFinalBlockAsync();

            var encoded = WebEncoders.Base64UrlEncode(ms.ToArray());
            return encoded;
        }

        public async Task<int> DecryptTestResultUrl(string testResultUrl)
        {
            using var aes = Aes.Create();
            aes.Key = new byte[] { 0x10, 0x01, 0x5f, 0x3a, 0x89, 0x7d, 0x6f, 0x04, 0x9c, 0xb6, 0x32, 0x45, 0x78, 0x4d, 0x2e, 0x1f };
            aes.IV = new byte[] { 0x21, 0x43, 0x56, 0x87, 0x10, 0xfd, 0xea, 0x1c, 0x9b, 0x2c, 0xae, 0x4d, 0x5f, 0x6a, 0x7b, 0x8c };

            var inputByteArray = WebEncoders.Base64UrlDecode(testResultUrl);
            using var ms = new MemoryStream(inputByteArray);
            using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using var reader = new StreamReader(cs);
            var decrypted = await reader.ReadToEndAsync();

            return int.Parse(decrypted);
        }

        public async Task<IEnumerable<TestResultDTO>> GetTestResultByUserId(string userId)
        {
            return mapper.Map<IEnumerable<TestResultDTO>>(await testResultsRepository.FilterByCriterionAsync(x => x.UserId == userId));
        }

        public async Task<IEnumerable<TestResultDTO>> GetTestResultByTestId(int testId)
        {
            return mapper.Map<IEnumerable<TestResultDTO>>(await testResultsRepository.FilterByCriterionIncludingTestAsync(x => x.TestId == testId));
        }

        public async Task<PaginationModel<TestResultDTO>> GetTestResultPerPage(int selectedPage, int itemsPerPage)
        {
            var filterPerPageRecord = await this.testResultsRepository.PaginationFilter(selectedPage,itemsPerPage);
            int totalCount = (filterPerPageRecord.DataCount + itemsPerPage - 1) / itemsPerPage;
            var data = mapper.Map<IEnumerable<TestResultDTO>>(filterPerPageRecord.FilterData);
            return new PaginationModel<TestResultDTO> { Data = data, ItemsPerPage = itemsPerPage, SelectedPage = selectedPage, TotalPageCount = totalCount};
        }

        public async Task<PaginationModel<TestResultDTO>> GetTestResultByUserNamePerPage(int selectedPage, int itemsPerPage, string userName)
        {
            var filterPerPageRecord = await this.testResultsRepository.PaginationFilterByUserName(selectedPage, itemsPerPage, userName);
            int totalCount = (filterPerPageRecord.DataCount + itemsPerPage - 1) / itemsPerPage;
            var data = this.mapper.Map<IEnumerable<TestResultDTO>>(filterPerPageRecord.FilterData);
            return new PaginationModel<TestResultDTO> { Data = data, ItemsPerPage = itemsPerPage, SelectedPage = selectedPage, TotalPageCount = totalCount };
        }

        public async Task<PaginationModel<TestResultDTO>> GetTestResultByUserIdPerPage(int selectedPage, int itemsPerPage, string userId)
        {
            var filterPerPageRecord = await this.testResultsRepository.PaginationFilterByUserId(selectedPage, itemsPerPage, userId);
            var data = this.mapper.Map<IEnumerable<TestResultDTO>>(filterPerPageRecord.FilterData);
            return new PaginationModel<TestResultDTO> { Data = data, ItemsPerPage = itemsPerPage, SelectedPage = selectedPage, TotalPageCount = filterPerPageRecord.DataCount };
        }

        public async Task<PaginationModel<TestResultDTO>> GetTestResultByTestIdPerPage(int selectedPage, int itemsPerPage, int testId)
        {
            var filterPerPageRecord = await this.testResultsRepository.PaginationFilterByTestId(selectedPage, itemsPerPage, testId);
            var data = this.mapper.Map<IEnumerable<TestResultDTO>>(filterPerPageRecord.FilterData);
            return new PaginationModel<TestResultDTO> { Data = data, ItemsPerPage = itemsPerPage, SelectedPage = selectedPage, TotalPageCount = filterPerPageRecord.DataCount };
        }

        public async Task<PaginationModel<TestDTO>> GetTestsFromSnapshots(int selectedPage, int itemsPerPage)
        {
            var tests = await this.testResultsRepository.GetTestsFromSnapshot(selectedPage, itemsPerPage);
            var data = this.mapper.Map<IEnumerable<TestDTO>>(tests.FilterData);
            return new PaginationModel<TestDTO> { Data = data, ItemsPerPage = itemsPerPage, SelectedPage = selectedPage, TotalPageCount = tests.DataCount };
        }
    }
}
