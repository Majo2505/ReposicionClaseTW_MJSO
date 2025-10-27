using EC4clase1.Models;

namespace EC4clase1.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task AddUser(User user);
    }
}
