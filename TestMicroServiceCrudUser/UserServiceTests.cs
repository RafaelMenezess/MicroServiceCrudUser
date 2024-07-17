using MicroServiceCrudUser.Models.Context;
using MicroServiceCrudUser.Models;
using MicroServiceCrudUser.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MicroServiceCrudUser.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace TestMicroServiceCrudUser;

public class UserServiceTests
{
    private readonly UserService _userService;
    private readonly MySQLContext _context;
    private readonly Mock<ILogger<UserService>> _loggerMock;


    public UserServiceTests()
    {
        var options = new DbContextOptionsBuilder<MySQLContext>()
            .UseInMemoryDatabase(databaseName: "user_db")
            .Options;
        _context = new MySQLContext(options);

        var inMemorySettings = new Dictionary<string, string> {
                {"Jwt:Key", "YourSuperSecretKey"},
                {"Jwt:Issuer", "yourdomain.com"},
                {"Jwt:Audience", "yourdomain.com"},
                {"Jwt:DurationInMinutes", "60"}
            };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
        
        _loggerMock = new Mock<ILogger<UserService>>();
        _userService = new UserService(_context, configuration, _loggerMock.Object);
    }

    [Fact]
    public async Task CreateUser()
    {
        var user = new User { Username = "testuser", PasswordHash = "password", Email = "test@example.com" };
        var result = await _userService.CreateUser(user);

        Assert.NotNull(result);
        Assert.Equal("testuser", result.Username);
        Assert.True(BCrypt.Net.BCrypt.Verify("password", result.PasswordHash));
    }

    [Fact]
    public async Task GetUserById()
    {
        var user = new User { Username = "testuser2", PasswordHash = "password", Email = "test2@example.com" };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var result = await _userService.GetUserById(user.Id);

        Assert.NotNull(result);
        Assert.Equal("testuser2", result.Username);
    }

    [Fact]
    public async Task UpdateUser()
    {
        var user = new User { Username = "testuser3", PasswordHash = "password", Email = "test3@example.com" };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        user.Email = "updated@example.com";
        var result = await _userService.UpdateUser(user);

        Assert.True(result);
        var updatedUser = await _userService.GetUserById(user.Id);
        Assert.Equal("updated@example.com", updatedUser.Email);
    }

    [Fact]
    public async Task DeleteUser()
    {
        var user = new User { Username = "testuser4", PasswordHash = "password", Email = "test4@example.com" };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var result = await _userService.DeleteUser(user.Id);

        Assert.True(result);
        var deletedUser = await _userService.GetUserById(user.Id);
        Assert.Null(deletedUser);
    }

}
