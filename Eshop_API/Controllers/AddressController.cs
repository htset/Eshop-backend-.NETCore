using Eshop_API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eshop_API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("_myAllowSpecificOrigins")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ItemContext _context;

        public AddressController(ItemContext context)
        {
            _context = context;
        }

        // GET: api/<AddressController>
        [HttpGet]
        public IEnumerable<Address> Get()
        {
            return _context.Addresses.ToList();
        }

        // GET api/<AddressController>/5
        [HttpGet("{userId}")]
        public IEnumerable<Address> GetByUserId(int userId)
        {
            return _context.Addresses.Where((addr) => addr.UserId == userId).ToList();
        }

        // POST api/<AddressController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AddressController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
