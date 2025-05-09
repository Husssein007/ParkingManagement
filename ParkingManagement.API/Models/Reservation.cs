using ParkingManagement.API.Models;

public class Reservation
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int ParkingSpotId { get; set; } 
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Status { get; set; }

    // Navigation Properties (si tu veux lier aux entités 'User' et 'ParkingSpot')
    public User User { get; set; }
    public ParkingSpot ParkingSpot { get; set; }  // Lié à ParkingSpotId
}
