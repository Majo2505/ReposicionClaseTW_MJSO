using EC4clase1.Data;
using EC4clase1.Models;
using Microsoft.EntityFrameworkCore;

namespace EC4clase1.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task AddUser(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
