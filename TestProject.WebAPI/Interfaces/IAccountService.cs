using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.DTO;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountDTO>> GetAllAccounts();
        Task<AccountDTO> GetAccountById(int id);
        Task CreateAccount(AccountDTO accountDTO);
        Task<bool> CanUserCreateAccount(AccountDTO accountDTO);
    }
}
