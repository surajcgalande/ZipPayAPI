using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Interfaces
{
    public interface IAccountService
    {
        Task<List<Account>> GetAllAccounts();
        Task<Account> GetAccountById(int id);
        Task CreateAccount(Account account);
        Task<bool> CanUserCreateAccount(Account account);
    }
}
