using System.ComponentModel.DataAnnotations;

namespace ParkingManagement.API.DTOs
{
    public class ParkingSpotDto
    {
        [Required]
        [StringLength(100)]
        public string Location { get; set; } // Localisation du spot de parking (ex. "A1", "B2", etc.)

        [Required]
        public bool IsAvailable { get; set; } // Indique si le spot est disponible

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // Le statut du spot de parking (ex. "Available", "Occupied", etc.)
    }
}
