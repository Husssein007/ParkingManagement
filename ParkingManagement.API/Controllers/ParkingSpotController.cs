using Microsoft.AspNetCore.Mvc;
using ParkingManagement.API.DTOs;
using ParkingManagement.API.Models;
using ParkingManagement.API.Services;

namespace ParkingManagement.API.Controllers
{
    [Route("api/parking-spots")]
    [ApiController]
    public class ParkingSpotController : ControllerBase
    {
        private readonly IParkingSpotService _parkingSpotService;

        public ParkingSpotController(IParkingSpotService parkingSpotService)
        {
            _parkingSpotService = parkingSpotService;
        }

        [HttpGet]
        public async Task<IActionResult> GetParkingSpots()
        {
            var parkingSpots = await _parkingSpotService.GetParkingSpots();
            return Ok(parkingSpots);
        }

        [HttpPost]
        public async Task<IActionResult> AddParkingSpot([FromBody] ParkingSpotDto parkingSpotDto)
        {
            var newParkingSpot = new ParkingSpot
            {
                Location = parkingSpotDto.Location, 
                IsAvailable = parkingSpotDto.IsAvailable,
                Status = parkingSpotDto.Status
            };

            await _parkingSpotService.AddParkingSpot(newParkingSpot);

            return CreatedAtAction(nameof(GetParkingSpotById), new { id = newParkingSpot.Id }, newParkingSpot);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParkingSpotById(int id)
        {
            var parkingSpot = await _parkingSpotService.GetParkingSpotById(id);
            if (parkingSpot == null)
            {
                return NotFound();
            }

            return Ok(parkingSpot);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParkingSpot(int id, [FromBody] ParkingSpotDto parkingSpotDto)
        {
            var parkingSpot = await _parkingSpotService.GetParkingSpotById(id);
            if (parkingSpot == null)
            {
                return NotFound();
            }

            parkingSpot.Location = parkingSpotDto.Location;
            parkingSpot.IsAvailable = parkingSpotDto.IsAvailable;
            parkingSpot.Status = parkingSpotDto.Status;

            await _parkingSpotService.UpdateParkingSpot(parkingSpot);

            return Ok(parkingSpot);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingSpot(int id)
        {
            await _parkingSpotService.DeleteParkingSpot(id);
            return NoContent();
        }

        [HttpPost("mark-occupied/{id}")]
        public async Task<IActionResult> MarkAsOccupied(int id)
        {
            var spot = await _parkingSpotService.GetParkingSpotById(id);
            if (spot == null) return NotFound();

            spot.IsAvailable = false;
            spot.Status = "occupied";

            await _parkingSpotService.UpdateParkingSpot(spot);

            return Ok(new { message = $"Parking spot {id} marked as occupied." });
        }
        [HttpPost("mark-available/{id}")]
        public async Task<IActionResult> MarkAsAvailable(int id)
        {
            var spot = await _parkingSpotService.GetParkingSpotById(id);
            if (spot == null) return NotFound();

            spot.IsAvailable = true;
            spot.Status = "available";

            await _parkingSpotService.UpdateParkingSpot(spot);

            return Ok(new { message = $"Parking spot {id} marked as available." });
        }


    }
}
