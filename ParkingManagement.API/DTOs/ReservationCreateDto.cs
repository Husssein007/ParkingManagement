namespace ParkingManagement.API.DTOs
{
    public class ReservationCreateDto
    {
        public Guid UserId { get; set; }
        public int ParkingSpotId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
    }
}
