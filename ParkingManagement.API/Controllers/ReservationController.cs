using Microsoft.AspNetCore.Mvc;
using ParkingManagement.API.DTOs;
using ParkingManagement.API.Models;
using ParkingManagement.API.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingManagement.API.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // ✅ Récupérer toutes les réservations
        [HttpGet]
        public async Task<IActionResult> GetReservations()
        {
            var reservations = await _reservationService.GetReservations();

            if (!reservations.Any())
            {
                return NotFound("No reservations found.");
            }

            // Convert to DTO with formatted dates
            var formattedReservations = reservations.Select(reservation => new
            {
                reservation.Id,
                reservation.UserId,
                reservation.ParkingSpotId,
                StartTime = reservation.StartTime.ToString("yyyy/MM/dd hh:mm tt"),  // Format AM/PM
                EndTime = reservation.EndTime.ToString("yyyy/MM/dd hh:mm tt"),
                reservation.Status
            });

            return Ok(formattedReservations);
        }

        [HttpGet("with-users")]
        public async Task<IActionResult> GetReservationsWithUser()
        {
            var reservations = await _reservationService.GetReservationsWithUser();
            return Ok(reservations);
        }


        // ✅ Pour l'historique de l'employé connecté
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetReservationsByUser(Guid userId)
        {
            var reservations = await _reservationService.GetReservationsByUser(userId);

            if (!reservations.Any())
            {
                return NotFound("Aucune réservation trouvée pour cet utilisateur.");
            }

            var formattedReservations = reservations.Select(reservation => new
            {
                reservation.Id,
                reservation.UserId,
                reservation.ParkingSpotId,
                StartTime = reservation.StartTime.ToString("yyyy/MM/dd hh:mm tt"),
                EndTime = reservation.EndTime.ToString("yyyy/MM/dd hh:mm tt"),
                reservation.Status,
                ParkingSpot = new
                {
                    reservation.ParkingSpot?.Id,
                    reservation.ParkingSpot?.Location
                }
            });

            return Ok(formattedReservations);
        }


        // ✅ Ajouter une nouvelle réservation
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> AddReservation([FromBody] ReservationCreateDto reservationDto)
        {
            if (reservationDto == null)
            {
                return BadRequest("Reservation data is invalid.");
            }

            try
            {
                var reservation = new Reservation
                {
                    UserId = reservationDto.UserId,
                    ParkingSpotId = reservationDto.ParkingSpotId,
                    StartTime = reservationDto.StartTime,
                    EndTime = reservationDto.EndTime,
                    Status = reservationDto.Status
                };

                var createdReservation = await _reservationService.AddReservation(reservation);

                if (createdReservation == null)
                {
                    return BadRequest("Could not create reservation.");
                }

                return CreatedAtAction(nameof(GetReservationById), new { id = createdReservation.Id }, createdReservation);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while creating reservation: {ex.Message}");
            }
        }


        // ✅ Récupérer une réservation par ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _reservationService.GetReservationById(id);

            if (reservation == null)
            {
                return NotFound($"Reservation with Id = {id} not found.");
            }

            var formattedReservation = new
            {
                reservation.Id,
                reservation.UserId,
                reservation.ParkingSpotId,
                StartTime = reservation.StartTime.ToString("yyyy/MM/dd hh:mm tt"),
                EndTime = reservation.EndTime.ToString("yyyy/MM/dd hh:mm tt"),
                reservation.Status
            };

            return Ok(formattedReservation);
        }

        // ✅ Mettre à jour une réservation
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] ReservationCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid reservation data.");
            }

            var reservation = await _reservationService.GetReservationById(id);

            if (reservation == null)
            {
                return NotFound($"Reservation with Id = {id} not found.");
            }

            try
            {
                reservation.UserId = dto.UserId;
                reservation.ParkingSpotId = dto.ParkingSpotId;
                reservation.StartTime = dto.StartTime;
                reservation.EndTime = dto.EndTime;
                reservation.Status = dto.Status;

                var updatedReservation = await _reservationService.UpdateReservation(reservation);

                if (updatedReservation == null)
                {
                    return BadRequest("Could not update reservation.");
                }

                return Ok(updatedReservation);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating reservation: {ex.Message}");
            }
        }


        // ✅ Supprimer une réservation
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _reservationService.GetReservationById(id);

            if (reservation == null)
            {
                return NotFound($"Reservation with Id = {id} not found.");
            }

            await _reservationService.DeleteReservation(id);
            return NoContent(); // HTTP 204 (sans contenu)cd 
        }
    }
}
