using MicroServiceCrudUser.Models;

namespace MicroServiceCrudUser.Services.IServices;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(int id);
    Task<User> CreateUser(User user);
    Task<bool> UpdateUser(User user);
    Task<bool> DeleteUser(int id);
    Task<string> GenerateToken(int id);
    Task<bool> ChangePassword(int userId, string newPassword);
}
