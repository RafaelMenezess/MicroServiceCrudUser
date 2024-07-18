using MicroServiceCrudUser.Models;
using MicroServiceCrudUser.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceCrudUser.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    /// <summary>
    /// Busca todos os usuários no banco
    /// </summary>
    /// <returns> Uma lista de usuários </returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        _logger.LogInformation("Buscando todos os usuários");

        return Ok(await _userService.GetAllUsers());
    }

    /// <summary>
    /// Retorna um usuário específico
    /// </summary>
    /// <param name="id"> Id do usuário </param>
    /// <returns> Detalhes de um usuário</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        _logger.LogInformation($"Buscando usuário Id: {id}");

        var user = await _userService.GetUserById(id);
        if (user == null)
        {
            _logger.LogInformation($"Usuário Id: {id} não encontrado");

            return NotFound();
        }
        return Ok(user);
    }

    /// <summary>
    /// Cria um novo usuário
    /// </summary>
    /// <param name="user"> Usuário que será criado </param>
    /// <returns> Usuário criado </returns>
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] User user)
    {
        _logger.LogInformation($"Criando novo usuário Nome: {user.Username}");

        var createdUser = await _userService.CreateUser(user);
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
    }

    /// <summary>
    /// Atualiza um usuário existente
    /// </summary>
    /// <param name="id"> Id do usuário que vai ser atualizado </param>
    /// <param name="user"> Dados atualizados do usuário </param>
    /// <returns> No Content </returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
    {
        if (id != user.Id)
        {
            _logger.LogInformation($"Id do usuário não confere com o Id do body da requisição");

            return BadRequest();
        }

        var result = await _userService.UpdateUser(user);
        if (!result)
        {
            _logger.LogInformation($"Não foi possível atualizar usuário Id: {id}");

            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Deleta um usuário
    /// </summary>
    /// <param name="id"> Id do usuário que vai ser deletado </param>
    /// <returns> No Content </returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        _logger.LogInformation($"Deletando usuário Id: {id}");

        var result = await _userService.DeleteUser(id);
        if (!result)
        {
            _logger.LogInformation($"Não foi possível deletar usuário Id: {id}");

            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Gera o token para um usuário
    /// </summary>
    /// <param name="id"> Id do usuário </param>
    /// <returns> Token </returns>
    [HttpPost("token")]
    public async Task<IActionResult> GenerateToken(int id)
    {
        _logger.LogInformation($"Gerando token para o usuário Id: {id}");

        var token = await _userService.GenerateToken(id);
        return Ok(new { token });
    }

    /// <summary>
    /// Alterar a senha do usuário
    /// </summary>
    /// <param name="userId"> Id do usuário que vai ser alterada a senha </param>
    /// <param name="newPassword"> Nova senha </param>
    /// <returns> No Content </returns>
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(int userId, string newPassword)
    {
        _logger.LogInformation($"Alterando a senha do usuário Id: {userId}");

        var result = await _userService.ChangePassword(userId, newPassword);
        if (!result)
        {
            _logger.LogInformation($"Não foi possível alterar a senha do usuário Id: {userId}");

            return NotFound();
        }

        return NoContent();
    }
}
