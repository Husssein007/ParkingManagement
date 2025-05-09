namespace ParkingManagement.API.DTOs
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public string Role { get; set; }
    }
}
