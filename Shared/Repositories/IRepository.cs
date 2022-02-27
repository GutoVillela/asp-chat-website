using Shared.Entities;

namespace Shared.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task CreateAsync(TEntity entity);
        Task<TEntity?> ReadAsync(int id);
        Task<IEnumerable<TEntity>> ReadAllAsync();
        void Delete(TEntity entity);
    }
}
