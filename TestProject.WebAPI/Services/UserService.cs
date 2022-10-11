using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.DTO;
using TestProject.WebAPI.Interfaces;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IAppDBContext _dbContext;
        private readonly IMapper _mapper;
        public UserService(IAppDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> CheckIfUserExists(string email)
        {
            if (string.IsNullOrEmpty(email)) return await Task.FromResult(true);

            var user = await _dbContext.User.FirstOrDefaultAsync(u => u.EmailId == email);
            if (user != null && user.Id > 0) return await Task.FromResult(true);
            return await Task.FromResult(false);
        }

        public async Task CreateUser(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            await _dbContext.User.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var users = await _dbContext.User.ToListAsync();
            if (users.Count == 0) return null;
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var user = await _dbContext.User.FindAsync(id);
            if (user == null) return null;
            return _mapper.Map<UserDTO>(user);
        }
    }
}
