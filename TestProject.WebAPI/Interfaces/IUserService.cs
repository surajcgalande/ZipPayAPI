using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<bool> CheckIfUserExists(string email);
        Task CreateUser(User user);
    }
}
