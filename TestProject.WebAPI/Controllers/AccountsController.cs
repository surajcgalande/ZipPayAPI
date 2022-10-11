using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TestProject.WebAPI.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TestProject.WebAPI.DTO;

namespace TestProject.WebAPI.Controllers
{
    /// <summary>
    /// Accounts Controller for actions to Add and List accounts
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService accountService;
        public AccountsController(IAccountService _accountService)
        {
            accountService = _accountService;
        }

        //GET: api/<AccountController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var accounts = await accountService.GetAllAccounts();
            if (!accounts.Any())
            {
                return NotFound();
            }
            return Ok(accounts);
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id == 0) return BadRequest();

            var account = await accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        // POST api/<AccountController>
        [HttpPost]
        public async Task<ActionResult> Post(AccountDTO accountDTO)
        {
            if (accountDTO == null) return BadRequest();
            if (!await accountService.CanUserCreateAccount(accountDTO)) { return BadRequest("User is not eligible to create account."); }

            await accountService.CreateAccount(accountDTO);
            return Ok();
        }
    }
}
