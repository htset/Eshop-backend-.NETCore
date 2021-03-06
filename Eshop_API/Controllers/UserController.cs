using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop_API.Models;
using Eshop_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eshop_API.Controllers
{
    [Authorize]
    [Route("api/users")]
    [EnableCors("_myAllowSpecificOrigins")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        // GET: api/User
        [HttpGet]
        public ActionResult Get()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<User> Get(int id)
        {
            var user = _userService.GetUser(id);
            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<Item> Post([FromBody] User user)
        {
            try
            {
                _userService.AddUser(user);
                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
