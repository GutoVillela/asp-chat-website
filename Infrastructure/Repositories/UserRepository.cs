using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext dbContext) 
        {
            _context = dbContext;
        }

        public async Task<User?> ReadAsync(string userId)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
        }
    }
}
