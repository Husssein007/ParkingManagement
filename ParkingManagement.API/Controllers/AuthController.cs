using Microsoft.AspNetCore.Mvc;
using ParkingManagement.API.DTOs;
using ParkingManagement.API.Models;
using ParkingManagement.API.Services;
using System.Threading.Tasks;
using ParkingManagement.API.Data; // 🆕 Ajoute ceci pour utiliser le contexte

namespace ParkingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ApplicationDbContext _context; // 🆕 Ajoute le contexte ici

        public AuthController(IAuthService authService, ApplicationDbContext context) // 🆕 Injecte-le ici
        {
            _authService = authService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                Position = request.Position,
                Role = request.Role,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password) // Hash du mot de passe
            };

            _context.Users.Add(user); // ✅ Maintenant `_context` est reconnu
            await _context.SaveChangesAsync(); // ✅ Plus d'erreur ici

            return Ok(new { message = "User registered successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _authService.Authenticate(request.Email, request.Password);
            if (user == null) return Unauthorized("Invalid credentials.");

            var token = _authService.GenerateJwtToken(user);
            return Ok(new { token = token, role = user.Role, id = user.Id });       }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    } 
}
