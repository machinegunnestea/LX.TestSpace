using AutoMapper;
using LX.TestSpace.Server.BLL.Interfaces;
using LX.TestSpace.Server.BLL.Models;
using LX.TestSpace.Server.DAL.Interfaces;
using LX.TestSpace.Server.DAL.Repositories;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LX.TestSpace.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswersService _answersService;
        ILogger<AnswersController> _logger;

        public AnswersController(IAnswersService answersService, ILogger<AnswersController> logger)
        {
            _answersService = answersService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin")]
        public async Task<IEnumerable<AnswerDTO>> Get()
        {
            var answers = await _answersService.GetAsync();
            return answers;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin")]
        public async Task Post([FromBody] AnswerDTO answerDTO)
        {
            await _answersService.CreateAsync(answerDTO);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin")]
        public async Task Put(int id, [FromBody] AnswerDTO answerDTO)
        {
            await _answersService.UpdateAsync(answerDTO);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin")]
        public async Task Delete(int id)
        {
            var answer = (await _answersService.GetAsync()).FirstOrDefault(x => x.Id == id);
            await _answersService.DeleteAsync(answer);
        }
    }
}
