using IdentityModel;
using LX.TestSpace.Server.BLL.Models;

namespace LX.TestSpace.Server.BLL.Interfaces
{
    public interface IEntityService<TEntityDTO>
        where TEntityDTO : class
    {
        Task<IEnumerable<TEntityDTO>> GetAsync();

        Task UpdateAsync(TEntityDTO entity);

        Task DeleteAsync(TEntityDTO entity);

        Task CreateAsync(TEntityDTO entity);
        Task<TEntityDTO> GetByIdAsync(int id);
    }
}
