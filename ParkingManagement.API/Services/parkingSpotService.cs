using ParkingManagement.API.Data;
using ParkingManagement.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ParkingManagement.API.Services
{
    public class ParkingSpotService : IParkingSpotService
    {
        private readonly ApplicationDbContext _context;

        public ParkingSpotService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ParkingSpot>> GetParkingSpots()
        {
            return await _context.ParkingSpots.ToListAsync();
        }

        public async Task<ParkingSpot> GetParkingSpotById(int id)
        {
            return await _context.ParkingSpots.FindAsync(id);
        }

        public async Task<ParkingSpot> AddParkingSpot(ParkingSpot parkingSpot)
        {
            _context.ParkingSpots.Add(parkingSpot);
            await _context.SaveChangesAsync();
            return parkingSpot;
        }

        public async Task<ParkingSpot> UpdateParkingSpot(ParkingSpot parkingSpot)
        {
            _context.ParkingSpots.Update(parkingSpot);
            await _context.SaveChangesAsync();
            return parkingSpot;
        }

        public async Task DeleteParkingSpot(int id)
        {
            var parkingSpot = await _context.ParkingSpots.FindAsync(id);
            if (parkingSpot != null)
            {
                _context.ParkingSpots.Remove(parkingSpot);
                await _context.SaveChangesAsync();
            }
        }
    }
}
