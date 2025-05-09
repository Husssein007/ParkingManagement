using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingManagement.API.Models
{
    public class UserRole
    {
        [Key] // ✅ Ajoute cette annotation si elle manque
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // ✅ Clé primaire

        public string Name { get; set; }
    }
}
