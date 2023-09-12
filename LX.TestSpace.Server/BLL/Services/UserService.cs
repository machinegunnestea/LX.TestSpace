using AutoMapper;
using LX.TestSpace.Server.BLL.Interfaces;
using LX.TestSpace.Server.BLL.Models;
using LX.TestSpace.Server.BLL.Models.PaginationModel;
using LX.TestSpace.Server.BLL.Models.Records;
using LX.TestSpace.Server.BLL.Profiles.MapperExtensions;
using LX.TestSpace.Server.DAL.Entities;
using LX.TestSpace.Server.DAL.Interfaces;
using LX.TestSpace.Server.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Duende.IdentityServer.Models.IdentityResources;

namespace LX.TestSpace.Server.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly ITestResultsRepository _testResultsRepository;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public UserService(UserManager<User> userManager, IMapper mapper, ITestResultsRepository testResultsRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this._testResultsRepository = testResultsRepository;
        }

        public async Task ChangeEmailAsync(string userId, string newEmail)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var emailExists = await userManager.FindByEmailAsync(newEmail);
                user.Email = newEmail;
                user.NormalizedEmail = newEmail.ToUpper();
                var result = await userManager.UpdateAsync(user);
            }
        }

        public async Task ChangePersonalInfoAsync(string userId, PersonalData personalData)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.Surname = personalData.Surname;
                user.Name = personalData.Name;
                var result = await userManager.UpdateAsync(user);
            }
        }

        public async Task ChangeRolesAsync(string userId, List<string> roles)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var addExceptedRoles = roles.Except(userRoles);
                var removeExceptedRoles = userRoles.Except(roles);
                await userManager.AddToRolesAsync(user, addExceptedRoles);
                await userManager.RemoveFromRolesAsync(user, removeExceptedRoles);
            }
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var users = mapper.MapUsersToDto(userManager, await userManager.Users.ToListAsync());
            return users;
        }

        public async Task<UserDTO> GetUserByIdAsync(string userId)
        {
            return mapper.MapUserToDto(userManager, await userManager.FindByIdAsync(userId));
        }

        public async Task<UserDTO> GetUserByEmailAsync(string userEmail)
        {
            return mapper.MapUserToDto(userManager, await userManager.FindByNameAsync(userEmail));
        }

        public async Task<PaginationModel<UserPersonalDataDto>> GetAllPersonalData(int page, int pageSize)
        {
            var users = await this.userManager.Users
                .OrderBy(e => e.Email)
                .Where(e => !string.IsNullOrWhiteSpace(e.Email))
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var usersPersonalData = this.mapper.Map<IEnumerable<UserPersonalDataDto>>(users);
            var totalUsersCount = await this.userManager.Users.CountAsync();
            var paginationModel = new PaginationModel<UserPersonalDataDto>
            {
                Data = usersPersonalData,
                TotalPageCount = totalUsersCount,
            };
            return paginationModel;
        }
    }
}
