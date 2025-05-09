using Microsoft.EntityFrameworkCore;
using ParkingManagement.API.Models;

namespace ParkingManagement.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }  // Ajout de la table ParkingSpot
        public DbSet<Message> Messages { get; set; }
        public DbSet<AdminAction> AdminActions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasDefaultValueSql("NEWID()"); // Définit un Guid par défaut pour la propriété Id de User
        }
    }
}
