using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManagement.API.Services
{
    public interface IDashboardService
    {
        Task<int> GetTotalParkingSpots();
        Task<int> GetAvailableSpots();
        Task<int> GetOccupiedSpots();
        Task<int> GetActiveUsers();
        Task<IEnumerable<object>> GetMonthlyReservations();
        Task<IEnumerable<string>> GetRecentActions();
    }
}
