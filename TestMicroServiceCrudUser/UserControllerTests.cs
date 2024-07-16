using MicroServiceCrudUser.Controllers;
using MicroServiceCrudUser.Models;
using MicroServiceCrudUser.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;

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
}
