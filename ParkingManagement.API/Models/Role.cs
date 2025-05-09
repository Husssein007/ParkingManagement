using ParkingManagement.API.Models;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Relation avec UserRole (Un Role peut avoir plusieurs utilisateurs)
    public ICollection<UserRole> UserRoles { get; set; }
}
