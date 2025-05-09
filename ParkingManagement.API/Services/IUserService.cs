using ParkingManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManagement.API.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(Guid id); 
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(Guid id);
        Task<User> GetUserByEmail(string email); 
    }
}
