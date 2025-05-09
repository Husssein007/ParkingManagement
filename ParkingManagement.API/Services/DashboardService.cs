using Microsoft.EntityFrameworkCore;
using ParkingManagement.API.Data;

namespace ParkingManagement.API.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalParkingSpots() =>
            await _context.ParkingSpots.CountAsync();

        public async Task<int> GetAvailableSpots() =>
            await _context.ParkingSpots.CountAsync(p => p.IsAvailable);

        public async Task<int> GetOccupiedSpots() =>
            await _context.ParkingSpots.CountAsync(p => !p.IsAvailable);

        public async Task<int> GetActiveUsers() =>
            await _context.Users.CountAsync(u => u.IsActive); // Assure-toi que IsActive existe

        public async Task<IEnumerable<object>> GetMonthlyReservations()
        {
            var year = DateTime.Now.Year;

            return await _context.Reservations
                .Where(r => r.StartTime.Year == year)
                .GroupBy(r => r.StartTime.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetRecentActions()
        {
            return await _context.AdminActions
                .OrderByDescending(a => a.Timestamp)
                .Take(10)
                .Select(a => a.Description)
                .ToListAsync();
        }

        public Task<int> GetTotalParking()
        {
            throw new NotImplementedException();
        }
    }
}
