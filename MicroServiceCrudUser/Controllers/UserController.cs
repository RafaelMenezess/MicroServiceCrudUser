using MicroServiceCrudUser.Models;
using MicroServiceCrudUser.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceCrudUser.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return Ok(await _userService.GetAllUsers());
    }
}
