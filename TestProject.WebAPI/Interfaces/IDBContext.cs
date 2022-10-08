using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Interfaces
{
    public interface IAppDBContext
    {
        DbSet<User> User { get; set; }
        DbSet<Account> Account { get; set; }
        DbSet<Log> ErrorLog { get; set; }

        Task<int> SaveChangesAsync();
    }
}
