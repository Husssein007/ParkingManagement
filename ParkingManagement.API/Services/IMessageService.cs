using ParkingManagement.API.DTOs;
using ParkingManagement.API.Models;

namespace ParkingManagement.API.Services.Interfaces
{
    public interface IMessageService
    {
        Task<bool> SendMessageAsync(MessageDto dto);
        Task<List<Message>> GetMessagesAsync();
    }
}
