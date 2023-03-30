using PB_EGS_Escalavel.Core.Entities;

namespace PB_EGS_Escalavel.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash);
    }
}
