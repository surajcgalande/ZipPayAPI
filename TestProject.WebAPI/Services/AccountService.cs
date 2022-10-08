using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Interfaces;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAppDBContext _dbContext;
        public AccountService(IAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> CanUserCreateAccount(Account account)
        {
            if(account == null) return Task.FromResult(false);
            if(account.UserId == 0) return Task.FromResult(false);

            var user = _dbContext.User.Find(account.UserId);
            if(user == null) return Task.FromResult(false);
            //Zip Pay allows credit for up to $1000, so if `monthly salary - monthly expenses` is less than $1000 we should not create an account for the user
            if ((user.MonthlySalary - user.MonthlyExpense) < 1000) return Task.FromResult(false);

            return Task.FromResult(true);
        }

        public async Task CreateAccount(Account account)
        {
            await _dbContext.Account.AddAsync(account);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Account> GetAccountById(int id)
        {
            return await _dbContext.Account.FindAsync(id);
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            return await _dbContext.Account.ToListAsync();
        }
    }
}
