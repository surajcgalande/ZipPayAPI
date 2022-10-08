using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Interfaces;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IAppDBContext _dbContext;
        public UserService(IAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CheckIfUserExists(string email)
        {
            if (string.IsNullOrEmpty(email)) return await Task.FromResult(true);

            var user = await _dbContext.User.FirstOrDefaultAsync(u => u.EmailId == email);
            if (user != null && user.Id > 0) return await Task.FromResult(true);
            return await Task.FromResult(false);
        }       

        public async Task CreateUser(User user)
        {
            await _dbContext.User.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
           return await _dbContext.User.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dbContext.User.FindAsync(id);
        }
    }
}
