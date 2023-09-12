using AutoMapper;
using LX.TestSpace.Server.BLL.Interfaces;
using LX.TestSpace.Server.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LX.TestSpace.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ITestService testService;
        private readonly ITestResultService testResultService;
        ILogger<TestsController> _logger;

        public TestsController(ITestService testService, ILogger<TestsController> logger, ITestResultService testResultService)
        {
            this.testResultService = testResultService;
            this.testService = testService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin")]
        public async Task<IEnumerable<TestDTO>> GetAllTests()
        {
            var tests = await testService.GetAsync();
            return tests;
        }

        [HttpGet("published")]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "User,Admin")]
        public async Task<IEnumerable<TestDTO>> GetPublishedTests()
        {
            var tests = await this.testService.GetPublishedTests();
            return tests;
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "User,Admin")]
        public async Task<TestDTO> Get(int id, [FromQuery] bool includeDetails = false)
        {
            var test = await testService.GetByIdAsync(id, User.IsInRole("Admin") && includeDetails);
            return test;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin")]
        public async Task<TestDTO> Post([FromBody] TestDTO testDTO)
        {
            var result = await testService.CreateAndReturnEntityAsync(testDTO);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin")]
        public async Task Put(int id, [FromBody] TestDTO testDTO)
        {
            await testService.UpdateAsync(testDTO);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin")]
        public async Task Delete(int id)
        {
            var test = await testService.GetByIdAsync(id);
            await testService.DeleteAsync(test);
        }

        [HttpGet("ByTestName")]
        public async Task<IEnumerable<TestDTO>> GetSearchedTests([FromQuery]string searchName="")
        {
            var tests = await this.testService.SearchTestByTitle(searchName);
            return tests;
        }
    }
}
