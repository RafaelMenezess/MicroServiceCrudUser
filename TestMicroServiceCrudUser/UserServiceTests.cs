using MicroServiceCrudUser.Models.Context;
using MicroServiceCrudUser.Models;
using MicroServiceCrudUser.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TestMicroServiceCrudUser;

public class UserServiceTests
{
    private readonly UserService _userService;
    private readonly MySQLContext _context;

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

        _userService = new UserService(_context, configuration);
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

}
