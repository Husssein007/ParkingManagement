using ParkingManagement.API.Models;
using System.Threading.Tasks;

namespace ParkingManagement.API.Services
{
    public interface IAuthService
    {
        Task<User?> Authenticate(string email, string password);
        string GenerateJwtToken(User user);
        Task<User> Register(User user);
    }
}
