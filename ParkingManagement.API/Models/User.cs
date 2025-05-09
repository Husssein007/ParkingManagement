using System;
using System.Collections.Generic; // Ajout de l'espace de noms pour ICollection
using System.ComponentModel.DataAnnotations;

namespace ParkingManagement.API.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } // Utilisation de GUID pour plus de sécurité et scalabilité

        [Required]
        [StringLength(100)] // Optionnel, mais ça peut limiter la taille du nom
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)] // Optionnel, mais ça peut limiter la taille du nom
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)] // Limite de taille d'email
        public string Email { get; set; }

        [Required]
        [Phone] // Ajout de la validation de téléphone
        public string Phone { get; set; }

        [Required]
        [StringLength(100)] // Limite de taille de la position
        public string Position { get; set; }

        [Required]
        [StringLength(100)] // Limite de taille du rôle
        public string Role { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        // Initialisation de la collection dans le constructeur pour éviter les valeurs nulles
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();  
        public bool IsActive { get; internal set; }
    }
}
