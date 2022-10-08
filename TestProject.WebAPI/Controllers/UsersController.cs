using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TestProject.WebAPI.Interfaces;
using System.Linq;
using TestProject.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace TestProject.WebAPI.Controllers
{
    /// <summary>
    /// Users Controller for actions to Add and List users
    /// </summary>
    [Route("api/[controller]")]   
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService _userService)
        {
            userService = _userService;
        }

        //GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var users = await userService.GetAllUsers();
            if (!users.Any())
            {
                return NotFound();
            }
            return Ok(users);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id == 0) return BadRequest();

            var user = await userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> Post(User user)
        {
            if (user == null) return BadRequest();
            if (user.Id > 0) return BadRequest("Id column is auto set. No need to specify it.");
            if(await userService.CheckIfUserExists(user.EmailId)) { return BadRequest("User Already Present with this email Id"); }

            await userService.CreateUser(user);
            return Ok();
        }
    }
}
