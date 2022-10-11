using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.DTO;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserById(int id);
        Task<bool> CheckIfUserExists(string email);
        Task CreateUser(UserDTO userDTO);
    }
}
