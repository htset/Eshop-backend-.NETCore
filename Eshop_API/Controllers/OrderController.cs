using Eshop_API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eshop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ItemContext _context;

        public OrderController(ItemContext context)
        {
            _context = context;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string GetOrder(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost]
        public ActionResult<Item> Post([FromBody] Order value)
        {
            value.OrderDate = DateTime.Now;

            var tempAddr = this._context.Addresses.Find(value.DeliveryAddressId);
            value.FirstName = tempAddr.FirstName;
            value.LastName = tempAddr.LastName;
            value.Street = tempAddr.Street;
            value.Zip = tempAddr.Zip;
            value.City = tempAddr.City;
            value.Country = tempAddr.Country;

            decimal tempTotalPrice = 0;
            foreach(var detail in value.OrderDetails)
            {
                int itemId = detail.ItemId;
                var tempItem = this._context.Items.Find(itemId);
                detail.ItemName = tempItem.Name;
                detail.ItemUnitPrice = tempItem.Price;
                detail.TotalPrice = detail.ItemUnitPrice * detail.Quantity;
                tempTotalPrice += detail.TotalPrice;
            }

            value.TotalPrice = tempTotalPrice;

            this._context.Orders.Add(value);
            this._context.SaveChanges();

            return CreatedAtAction(nameof(GetOrder), new { id = value.Id }, value);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
