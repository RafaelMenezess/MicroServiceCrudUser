using MicroServiceCrudUser.Models;
using MicroServiceCrudUser.Models.Context;
using MicroServiceCrudUser.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace MicroServiceCrudUser.Services;

public class UserService : IUserService
{
    private readonly MySQLContext _context;
    private readonly IConfiguration _configuration;
    public UserService(MySQLContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserById(int id)
    {
        return await _context.Users.FindAsync(id);
    }
    public async Task<User> CreateUser(User user)
    {
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
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
