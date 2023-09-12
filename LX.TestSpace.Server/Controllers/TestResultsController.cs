using AutoMapper;
using LX.TestSpace.Server.BLL.Interfaces;
using LX.TestSpace.Server.BLL.Models;
using LX.TestSpace.Server.BLL.Models.PaginationModel;
using LX.TestSpace.Server.BLL.Services;
using LX.TestSpace.Server.DAL.Entities;
using LX.TestSpace.Server.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LX.TestSpace.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestResultsController : ControllerBase
    {
        ITestResultsRepository testResultsRepository;
        IMapper _mapper;
        ILogger<TestResult> _logger;
        ITestResultService testResultService;
        UserManager<User> _userManager;

        public TestResultsController(ITestResultsRepository testResultsRepository, IMapper mapper, ILogger<TestResult> logger, ITestResultService testResultService, UserManager<User> userManager)
        {
            this.testResultsRepository = testResultsRepository;
            _mapper = mapper;
            _logger = logger;
            this.testResultService = testResultService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin,User")]
        public async Task<PaginationModel<TestResultDTO>> Get(int selectedPage, int itemsPerPage)
        {
            var paginationModel = await testResultService.GetTestResultPerPage(selectedPage, itemsPerPage);
            return paginationModel;
        }

        [HttpGet("userTestResults")]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin,User")]
        public async Task<PaginationModel<TestResultDTO>> GetTestResultsByUserName(int selectedPage, int itemsPerPage, string userName)
        {
            var paginationModel = await testResultService.GetTestResultByUserNamePerPage(selectedPage, itemsPerPage, userName);
            return paginationModel;
        }

        [HttpGet("Users")]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin")]
        public async Task<PaginationModel<TestResultDTO>> GetTestResultsByUserId(int selectedPage, int itemsPerPage, string userId)
        {
            var paginationModel = await this.testResultService.GetTestResultByUserIdPerPage(selectedPage, itemsPerPage, userId);
            return paginationModel;
        }

        [HttpGet("Tests")]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin")]
        public async Task<PaginationModel<TestResultDTO>> GetTestResultsByUserId(int selectedPage, int itemsPerPage, int testId)
        {
            var paginationModel = await this.testResultService.GetTestResultByTestIdPerPage(selectedPage, itemsPerPage, testId);
            return paginationModel;
        }

        [HttpGet("TestSnapshots")]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin")]
        public async Task<PaginationModel<TestDTO>> GetTestsFromSnapshots(int selectedPage, int itemsPerPage)
        {
            var paginationModel = await this.testResultService.GetTestsFromSnapshots(selectedPage, itemsPerPage);
            return paginationModel;
        }

        [HttpGet("start/{testId}/{userName}")]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin,User")]
        public async Task<TestResultDTO> TestStart(int testId, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var testResult = _mapper.Map<TestResultDTO>(await testResultService.TestStart(testId, user.Id));
            return testResult;
        }

        [HttpPut("intermediate/{testResultId}/{questionId}")]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin,User")]
        public async Task SaveIntermediateResult(int testResultId, int questionId, QuestionSnapshot questionSnapshot)
        {
            await testResultService.SaveIntermediateResult(questionSnapshot, questionId, testResultId);
        }

        [HttpGet("end/{testResultId}")]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin,User")]
        public async Task EndTestExecution(int testResultId)
        {
            await testResultService.EndTestExecution(testResultId);
        }

        [HttpPost]
        public void Post([FromBody] TestResultDTO testResultDTO)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] TestResultDTO testResultDTO)
        {
        }
        [HttpGet("{id}")]
        public async Task<TestResultDTO> Get(int id)
        {
            var tests = await testResultService.GetByIdAsync(id);
            return tests;
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("shared/encrypt/{id}")]
        public async Task<string> EncryptUrl(int id)
        {
            var encrId = await testResultService.EncryptTestResultUrl(id);
            return encrId;
        }

        [HttpGet("shared/{url}")]
        public async Task<TestResultDTO> DecryptUrl(string url)
        {
            string real = System.Web.HttpUtility.UrlDecode(url);
            var id = await testResultService.DecryptTestResultUrl(real);
            return await testResultService.GetByIdAsync(id);
        }

        [HttpGet("testResults/{id}")]
        public async Task<IEnumerable<TestResultDTO>> GetTestResultsByUserId(string id)
        {
            return await testResultService.GetTestResultByUserId(id);
        }

        [HttpGet("Tests/{testId}")]
        public async Task<IEnumerable<TestResultDTO>> GetTestResultByTest(int testId)
        {
            return await testResultService.GetTestResultByTestId(testId);
        }
    }
}
