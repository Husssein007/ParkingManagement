using Microsoft.AspNetCore.Mvc;
using ParkingManagement.API.Services;

namespace ParkingManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("total-parking-spots")]
        public async Task<IActionResult> GetTotalParkingSpots()
            => Ok(await _dashboardService.GetTotalParkingSpots());

        [HttpGet("available-spots")]
        public async Task<IActionResult> GetAvailableSpots()
            => Ok(await _dashboardService.GetAvailableSpots());

        [HttpGet("occupied-spots")]
        public async Task<IActionResult> GetOccupiedSpots()
            => Ok(await _dashboardService.GetOccupiedSpots());

        [HttpGet("active-users")]
        public async Task<IActionResult> GetActiveUsers()
            => Ok(await _dashboardService.GetActiveUsers());

        [HttpGet("monthly-reservations")]
        public async Task<IActionResult> GetMonthlyReservations()
            => Ok(await _dashboardService.GetMonthlyReservations());

        [HttpGet("recent-actions")]
        public async Task<IActionResult> GetRecentActions()
            => Ok(await _dashboardService.GetRecentActions());
    }
}
