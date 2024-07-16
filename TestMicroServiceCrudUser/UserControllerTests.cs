using MicroServiceCrudUser.Controllers;
using MicroServiceCrudUser.Models;
using MicroServiceCrudUser.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TestMicroServiceCrudUser;

public class UserControllerTests
{
    private readonly UsersController _controller;
    private readonly Mock<IUserService> _mockUserService;

    public UserControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _controller = new UsersController(_mockUserService.Object);
    }

    [Fact]
    public async Task GetUsers()
    {
        // Arrange
        var users = new List<User>
            {
                new User { Id = 1, Username = "user1" },
                new User { Id = 2, Username = "user2" }
            };
        _mockUserService.Setup(service => service.GetAllUsers()).ReturnsAsync(users);

        // Act
        var result = await _controller.GetUsers();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<User>>>(result);
        var returnValue = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnUsers = Assert.IsType<List<User>>(returnValue.Value);
        Assert.Equal(2, returnUsers.Count);
    }

    [Fact]
    public async Task GetUserById()
    {
        // Arrange
        var user = new User { Id = 1, Username = "user1" };
        _mockUserService.Setup(service => service.GetUserById(1)).ReturnsAsync(user);

        // Act
        var result = await _controller.GetUser(1);

        // Assert
        var actionResult = Assert.IsType<ActionResult<User>>(result);
        var returnValue = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnUser = Assert.IsType<User>(returnValue.Value);
        Assert.Equal("user1", returnUser.Username);
    }

    [Fact]
    public async Task CreateUser()
    {
        // Arrange
        var user = new User { Id = 1, Username = "user1" };
        _mockUserService.Setup(service => service.CreateUser(user)).ReturnsAsync(user);

        // Act
        var result = await _controller.CreateUser(user);

        // Assert
        var actionResult = Assert.IsType<ActionResult<User>>(result);
        var returnValue = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        var returnUser = Assert.IsType<User>(returnValue.Value);
        Assert.Equal("user1", returnUser.Username);
    }

    [Fact]
    public async Task UpdateUser()
    {
        // Arrange
        var user = new User { Id = 1, Username = "user1", Email = "old@example.com" };
        _mockUserService.Setup(service => service.UpdateUser(user)).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateUser(1, user);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteUser()
    {
        // Arrange
        _mockUserService.Setup(service => service.DeleteUser(1)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteUser(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
