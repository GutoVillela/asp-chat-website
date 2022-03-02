namespace Shared.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
