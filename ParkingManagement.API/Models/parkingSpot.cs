using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingManagement.API.Models
{
    public class ParkingSpot
    {
        [Key]
        public int Id { get; set; } // Identifiant unique pour chaque place de parking

        [Required]
        public string Location { get; set; } // Emplacement de la place de parking (par exemple, "A1", "B2", etc.)

        [Required]
        public bool IsAvailable { get; set; } // Indique si la place est disponible ou non

        [Required]
        public string Status { get; set; } // Statut de la place de parking ( "Available", "Occupied")
    }
}
