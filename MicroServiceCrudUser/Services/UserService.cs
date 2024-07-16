using MicroServiceCrudUser.Models;
using MicroServiceCrudUser.Services.IServices;

namespace MicroServiceCrudUser.Services;

public class UserService : IUserService
{
    public Task<IEnumerable<User>> GetAllUsers()
    {
        throw new NotImplementedException();
    }
    public Task<User> GetUserById(int id)
    {
        throw new NotImplementedException();
    }
    public Task<User> CreateUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
    public Task<bool> ChangePassword(int userId, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<string> GenerateToken(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUser(int id)
    {
        throw new NotImplementedException();
    }
}
