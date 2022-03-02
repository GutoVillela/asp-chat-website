using Infrastructure.Persistence.DataContext;
using Shared.Repositories;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationContext;

        public UnitOfWork(ApplicationDbContext applicationContext) => _applicationContext = applicationContext;

        public async Task CommitAsync() => await _applicationContext.SaveChangesAsync();
    }
}
