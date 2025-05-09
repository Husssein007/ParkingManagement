namespace ParkingManagement.API.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ParkingSpotId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }

        // ✅ Pour l'affichage
        public string UserFirstName { get; set; }
        public string UserEmail { get; set; }
    }
}
