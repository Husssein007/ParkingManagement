using System.ComponentModel.DataAnnotations;

namespace ParkingManagement.API.DTOs
{
    public class UserDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; } // ✅ Assure-toi qu'il est bien là

        [Required]
        public string Position { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string Password { get; set; }
    }

}
