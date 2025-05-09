namespace ParkingManagement.API.Models
{
    public class AdminAction
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

}
