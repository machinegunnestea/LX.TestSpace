namespace LX.TestSpace.Server.DAL.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task CreateAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(int id);
    }
}
