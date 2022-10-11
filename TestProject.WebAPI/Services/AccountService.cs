using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.DTO;
using TestProject.WebAPI.Interfaces;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAppDBContext _dbContext;
        private readonly IMapper _mapper;
        public AccountService(IAppDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<bool> CanUserCreateAccount(AccountDTO accountDTO)
        {
            if(accountDTO == null) return Task.FromResult(false);
            if(accountDTO.UserId == 0) return Task.FromResult(false);

            var user = _dbContext.User.Find(accountDTO.UserId);
            if(user == null) return Task.FromResult(false);
            //Zip Pay allows credit for up to $1000, so if `monthly salary - monthly expenses` is less than $1000 we should not create an account for the user
            if ((user.MonthlySalary - user.MonthlyExpense) < 1000) return Task.FromResult(false);

            return Task.FromResult(true);
        }

        public async Task CreateAccount(AccountDTO accountDTO)
        {
            var account = _mapper.Map<Account>(accountDTO);
            await _dbContext.Account.AddAsync(account);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<AccountDTO> GetAccountById(int id)
        {
            var account = await _dbContext.Account.FindAsync(id);

            if(account == null) return null;

            return _mapper.Map<AccountDTO>(account);
        }

        public async Task<List<AccountDTO>> GetAllAccounts()
        {
            var accounts = await _dbContext.Account.ToListAsync();

            if(accounts.Count == 0) return null;

            return _mapper.Map<List<AccountDTO>>(accounts);
        }
    }
}
