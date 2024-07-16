using MicroServiceCrudUser.Models;
using MicroServiceCrudUser.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceCrudUser.Controllers;

[ApiController]
[Route("api/[controller]")]
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

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        var createdUser = await _userService.CreateUser(user);
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        var result = await _userService.UpdateUser(user);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _userService.DeleteUser(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("token")]
    public async Task<IActionResult> GenerateToken(User user)
    {
        var token = await _userService.GenerateToken(user);
        return Ok(new { token });
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(int userId, string newPassword)
    {
        var result = await _userService.ChangePassword(userId, newPassword);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
