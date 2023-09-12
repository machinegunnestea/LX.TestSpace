using AutoMapper;
using LX.TestSpace.Server.BLL.Interfaces;
using LX.TestSpace.Server.BLL.Models;
using LX.TestSpace.Server.BLL.Models.Records;
using LX.TestSpace.Server.BLL.Profiles.MapperExtensions;
using LX.TestSpace.Server.BLL.Services;
using LX.TestSpace.Server.DAL.Entities;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LX.TestSpace.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        ILogger<AnswersController> _logger;
        IMapper _mapper;
        UserManager<User> _userManager;
        private readonly IUserService _userService;

        public UsersController(ILogger<AnswersController> logger, IMapper mapper, UserManager<User> userManager, IUserService userService)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _userService = userService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin")]
        public async Task<IEnumerable<UserDTO>> Get()
        {
            return await _userService.GetUsersAsync();
        }

        [HttpGet("PersonalData")]
        [Authorize(AuthenticationSchemes = Duende.IdentityServer.IdentityServerConstants.LocalApi.PolicyName, Roles = "Admin")]
        public async Task<IActionResult> GetPersonalData([FromQuery] int page = 0, [FromQuery] int pageSize = 10)
        {
            var users = await this._userService.GetAllPersonalData(page, pageSize);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<UserDTO> GetById(string id)
        {
            return await _userService.GetUserByIdAsync(id);
        }

        [HttpPut("{userId}")]
        public async Task<UserDTO> UpdateUser(UserDTO user, string userId)
        {
            await _userService.ChangeEmailAsync(userId, user.Email);
            await _userService.ChangePersonalInfoAsync(userId, new PersonalData(user.Name, user.Surname));
            await _userService.ChangeRolesAsync(userId, user.UserRoles.ToList());
            return await _userService.GetUserByIdAsync(userId);
        }

        [HttpGet("User/{userEmail}")]
        public async Task<UserDTO> GetByEmail(string userEmail)
        {
            return await _userService.GetUserByEmailAsync(userEmail);
        }
    }
}
