using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TestProject.WebAPI.Interfaces;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Data
{
    public class AppDBContext : DbContext, IAppDBContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Account> Account { get; set; }

        public DbSet<Log> ErrorLog { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Seed data
            builder.Entity<User>().HasData(new User { Id = 1, EmailId = "suraj.galande@gmail.com", MonthlyExpense = 1000, MonthlySalary = 100000, Name = "Suraj Galande" });
            base.OnModelCreating(builder);
        }
    }
}
