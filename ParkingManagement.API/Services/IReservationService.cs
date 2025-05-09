using ParkingManagement.API.DTOs;
using ParkingManagement.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManagement.API.Services
{
    public interface IReservationService
    {
        Task<List<Reservation>> GetReservations(); // Récupérer toutes les réservations
        Task<Reservation> GetReservationById(int id); // Récupérer une réservation par ID
        Task<Reservation> AddReservation(Reservation reservation); // Ajouter une réservation
        Task<Reservation> UpdateReservation(Reservation reservation); // Mettre à jour une réservation
        Task DeleteReservation(int id); // Supprimer une réservation
        Task<IEnumerable<Reservation>> GetReservationsByUser(Guid userId);
        Task<IEnumerable<ReservationWithUserDto>> GetReservationsWithUser();
    }
}
