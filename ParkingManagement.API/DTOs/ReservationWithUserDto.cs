namespace ParkingManagement.API.DTOs
{
    public class ReservationWithUserDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ParkingSpotId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public string? SpotLocation { get; set; }
    }
}
