using AuthMicroservice.Data;
using AuthMicroservice.Models;
using AuthMicroservice.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthMicroservice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenService _jwtService;
    private readonly AuthDbContext _db;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IJwtTokenService jwtService, AuthDbContext db, ILogger<AuthController> logger)
    {
        _jwtService = jwtService;
        _db = db;
        _logger = logger;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto request)
    {
        var user = _db.Users.FirstOrDefault(u => u.Username == request.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            _logger.LogWarning("Failed login attempt for user {Username}.", request.Username);
            return Unauthorized(new { Message = "Неверный логин или пароль" });
        }

        _logger.LogInformation("User {Username} successfully authenticated.", user.Username);

        var token = _jwtService.GenerateEncryptedToken(user.Username);

        return Ok(new TokenResponseDto { Token = token });
    }
}
