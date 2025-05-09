using ParkingManagement.API.DTOs;
using ParkingManagement.API.Models;
using ParkingManagement.API.Services.Interfaces;
using ParkingManagement.API.Data;
using Microsoft.EntityFrameworkCore; // ❗️ A ajouter pour utiliser ToListAsync

namespace ParkingManagement.API.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;

        public MessageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SendMessageAsync(MessageDto dto)
        {
            try
            {
                var message = new Message
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Content = dto.Content,
                    SentAt = DateTime.UtcNow
                };

                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'enregistrement du message : {ex.Message}");
                return false;
            }
        }

        public async Task<List<Message>> GetMessagesAsync()
        {
            return await _context.Messages
                .OrderByDescending(m => m.SentAt)
                .ToListAsync();
        }
    }
}
