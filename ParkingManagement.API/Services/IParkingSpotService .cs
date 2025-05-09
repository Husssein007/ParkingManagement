using ParkingManagement.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManagement.API.Services
{
    public interface IParkingSpotService
    {
        Task<IEnumerable<ParkingSpot>> GetParkingSpots();
        Task<ParkingSpot> GetParkingSpotById(int id);
        Task<ParkingSpot> AddParkingSpot(ParkingSpot parkingSpot);
        Task<ParkingSpot> UpdateParkingSpot(ParkingSpot parkingSpot);
        Task DeleteParkingSpot(int id);
    }
}
