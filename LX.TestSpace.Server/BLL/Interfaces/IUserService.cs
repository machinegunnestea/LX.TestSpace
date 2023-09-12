using LX.TestSpace.Server.BLL.Models;
using LX.TestSpace.Server.BLL.Models.PaginationModel;
using LX.TestSpace.Server.BLL.Models.Records;

namespace LX.TestSpace.Server.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsersAsync();

        Task ChangePersonalInfoAsync(string userId, PersonalData personalData);

        Task ChangeEmailAsync(string userId, string newEmail);

        Task ChangeRolesAsync(string userId, List<string> roles);

        Task<UserDTO> GetUserByIdAsync(string userId);

        Task<UserDTO> GetUserByEmailAsync(string userEmail);
        
        Task<PaginationModel<UserPersonalDataDto>> GetAllPersonalData(int page, int pageSize);
    }
}
