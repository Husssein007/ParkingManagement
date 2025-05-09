using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingManagement.API.DTOs;
using ParkingManagement.API.Models;
using ParkingManagement.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingManagement.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    //[Authorize] // 🔐 Active l'authentification (à retirer pour les tests si besoin)
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // ✅ Récupérer tous les utilisateurs
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();

            // Convertir User en UserResponseDto pour éviter de retourner les mots de passe
            var userDtos = users.Select(user => new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Position = user.Position,
                Role = user.Role
            });

            return Ok(userDtos);
        }

        // ✅ Ajouter un utilisateur
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            if (userDto == null || string.IsNullOrEmpty(userDto.Email))
            {
                return BadRequest("User data is invalid.");
            }

            // Vérifier si l'email existe déjà
            var existingUser = await _userService.GetUserByEmail(userDto.Email);
            if (existingUser != null)
            {
                return Conflict("Email already exists.");
            }

            // Création de l'utilisateur
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Phone = userDto.Phone,
                Position = userDto.Position,
                Role = userDto.Role,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password) // Hachage du mot de passe
            };

            await _userService.AddUser(newUser);

            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, new UserResponseDto
            {
                Id = newUser.Id,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Phone = newUser.Phone,
                Position = newUser.Position,
                Role = newUser.Role
            });
        }

        // ✅ Récupérer un utilisateur par ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound($"User with Id = {id} not found.");
            }

            return Ok(new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Position = user.Position,
                Role = user.Role
            });
        }

        // ✅ Modifier un utilisateur
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserDto userDto)
        {
            var existingUser = await _userService.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound($"User with Id = {id} not found.");
            }

            // Mettre à jour les champs
            existingUser.FirstName = userDto.FirstName;
            existingUser.LastName = userDto.LastName;
            existingUser.Email = userDto.Email;
            existingUser.Phone = userDto.Phone;
            existingUser.Position = userDto.Position;
            existingUser.Role = userDto.Role;

            if (!string.IsNullOrEmpty(userDto.Password))
            {
                existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            }

            var updatedUser = await _userService.UpdateUser(existingUser);
            return Ok(new UserResponseDto
            {
                Id = updatedUser.Id,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                Email = updatedUser.Email,
                Phone = updatedUser.Phone,
                Position = updatedUser.Position,
                Role = updatedUser.Role
            });
        }

        // ✅ Supprimer un utilisateur
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userService.DeleteUser(id);
            if (user == null)
            {
                return NotFound($"User with Id = {id} not found.");
            }
            return NoContent(); // HTTP 204
        }
    }
}
