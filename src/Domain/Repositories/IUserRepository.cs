using Domain.Entities;
using Shared.Repositories;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> ReadAsync(string userId);
    }
}
