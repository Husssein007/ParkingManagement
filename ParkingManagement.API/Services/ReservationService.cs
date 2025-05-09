using Microsoft.EntityFrameworkCore;
using ParkingManagement.API.Data;
using ParkingManagement.API.DTOs;
using ParkingManagement.API.Models;

namespace ParkingManagement.API.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetReservations()
        {
            // On inclut les entités liées User et ParkingSpot pour récupérer les informations complètes
            return await _context.Reservations
                .Include(r => r.User) // Assurer que l'utilisateur est inclus
                .Include(r => r.ParkingSpot) // Assurer que le spot de parking est inclus
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUser(Guid userId)
        {
            return await _context.Reservations
                .Include(r => r.ParkingSpot) // Pour ramener la localisation de la place
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }


        public async Task<Reservation> GetReservationById(int id)
        {
            return await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.ParkingSpot)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Reservation> AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);

            // ✅ N'occupe la place QUE si la réservation est confirmée
            if (reservation.Status == "confirmed")
            {
                var spot = await _context.ParkingSpots.FindAsync(reservation.ParkingSpotId);
                if (spot != null)
                {
                    spot.Status = "occupied";
                }
            }

            await _context.SaveChangesAsync();
            return reservation;
        }


        public async Task<Reservation> UpdateReservation(Reservation reservation)
        {
            _context.Reservations.Update(reservation);

            var spot = await _context.ParkingSpots.FindAsync(reservation.ParkingSpotId);
            if (spot != null)
            {
                if (reservation.Status == "confirmed")
                    spot.Status = "occupied";
                else if (reservation.Status == "cancelled")
                    spot.Status = "available";
            }
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation != null)
            {
                // ✅ Libérer la place occupée
                var spot = await _context.ParkingSpots.FindAsync(reservation.ParkingSpotId);
                if (spot != null && reservation.Status == "confirmed")
                {
                    spot.Status = "available";
                }

                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }



        public async Task<IEnumerable<ReservationWithUserDto>> GetReservationsWithUser()
        {
            return await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.ParkingSpot)
                .Select(r => new ReservationWithUserDto
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    ParkingSpotId = r.ParkingSpotId,
                    StartTime = r.StartTime,
                    EndTime = r.EndTime,
                    Status = r.Status,
                    FirstName = r.User.FirstName,
                    Email = r.User.Email,
                    SpotLocation = r.ParkingSpot.Location
                })
                .ToListAsync();
        }



    }
}
