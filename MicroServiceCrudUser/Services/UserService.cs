using MicroServiceCrudUser.Models;
using MicroServiceCrudUser.Models.Context;
using MicroServiceCrudUser.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MicroServiceCrudUser.Services;

public class UserService : IUserService
{
    private readonly MySQLContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserService> _logger;

    public UserService(MySQLContext context, IConfiguration configuration, ILogger<UserService> logger)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;
    }
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        try
        {
            return await _context.Users.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao buscar todos os usuários, erro: {ex.Message}");

            return null;
        }
    }

    public async Task<User> GetUserById(int id)
    {
        try
        {
            return await _context.Users.FindAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao buscar o usuário Id: {id}, erro: {ex.Message}");

            return null;
        }
    }
    public async Task<User> CreateUser(User user)
    {
        try
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao criar o usuário Nome: {user.Username}, erro: {ex.Message}");

            return null;
        }
    }

    public async Task<bool> UpdateUser(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!UserExists(user.Id))
            {
                _logger.LogInformation($"Usuário Id: {user.Id} não existe, erro: {ex.Message}");

                return false;
            }
            else
            {
                _logger.LogError($"Erro: {ex.Message}");

                return false;
            }
        }
    }
    public async Task<bool> DeleteUser(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogInformation($"Usuário Id: {id} não encontrado");

                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao deletar o usuário Id: {id}, erro: {ex.Message}");

            return false;
        }
    }


    public async Task<string> GenerateToken(int id)
    {
        try
        {
            User user = await GetUserById(id);
            if (user == null)
            {
                _logger.LogInformation($"Usuário Id: {id} não encontrado");

                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:DurationInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao gerar token para o usuário Id: {id}, erro: {ex.Message}");

            return null;
        }
    }

    public async Task<bool> ChangePassword(int userId, string newPassword)
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                _logger.LogInformation($"Usuário Id: {userId} não encontrado");

                return false;
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao atualizar a senha do usuário Id: {userId}, erro: {ex.Message}");

            return false;

        }
    }


    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }
}
