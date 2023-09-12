using AutoMapper;
using LX.TestSpace.Server.BLL.Interfaces;
using LX.TestSpace.Server.BLL.Models;
using LX.TestSpace.Server.BLL.Services;
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
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService questionService;
        ILogger<QuestionsController> _logger;

        public QuestionsController(IQuestionService questionService, ILogger<QuestionsController> logger)
        {
            this.questionService = questionService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin")]
        public async Task<IEnumerable<QuestionDTO>> Get()
        {
            var questions = await questionService.GetAsync();
            return questions;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin")]
        public async Task<QuestionDTO> PostAndReturn([FromBody] QuestionDTO questionDTO)
        {
            return await questionService.CreateAndReturnAsync(questionDTO);
        }

        [HttpPost("{id}/image")]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin")]
        public async Task<string> PostImage(int id, IFormFile image)
        {
            var imageStream = image.OpenReadStream();
            var imageExtension = Path.GetExtension(image.FileName);
            return await questionService.AttachImageToQuestionAsync(id, imageStream, imageExtension);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin")]
        public async Task Put(int id, [FromBody] QuestionDTO questionDTO)
        {
            await questionService.UpdateAsync(questionDTO);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin")]
        public async Task Delete(int id)
        {
            var question = (await questionService.GetAsync()).FirstOrDefault(x => x.Id == id);
            await questionService.DeleteAsync(question);
        }
    }
}
