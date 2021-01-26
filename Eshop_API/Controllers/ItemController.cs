using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop_API.Models;
using Microsoft.AspNetCore.Cors;
using Eshop_API.Controllers;

namespace ItemApi.Controllers
{
    [Route("api/items")]
    [EnableCors("_myAllowSpecificOrigins")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemContext _context;

        public ItemController(ItemContext context)
        {
            _context = context;
/*
            if (_context.Items.Count() == 0)
            {
                // Create a new ItemItem if collection is empty,
                // which means you can't delete all ItemItems.
                _context.Items.Add(new Item { Name = "Item1" });
                _context.SaveChanges();
            }
*/
        }

        /*
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems([FromQuery] QueryStringParameters qsParameters)
        {

            IQueryable<Item> itemsQ = _context.Items
                                        .OrderBy(on => on.Id);

            if (qsParameters.Name != null && !qsParameters.Name.Trim().Equals(string.Empty))
                itemsQ = itemsQ.Where(item => item.Name.ToLower().Contains(qsParameters.Name.Trim().ToLower()));

            if (qsParameters.Category != null && !qsParameters.Category.Trim().Equals(string.Empty))
            {
                string[] categories = qsParameters.Category.Split('#');
                itemsQ = itemsQ.Where(item => categories.Contains(item.Category));
            }

            itemsQ = itemsQ.Skip((qsParameters.PageNumber - 1) * qsParameters.PageSize)
                                        .Take(qsParameters.PageSize);

            return await itemsQ.ToListAsync();
        }
*/
        [HttpGet]
        public async Task<ActionResult<ItemPayload>> GetItems([FromQuery] QueryStringParameters qsParameters)
        {

            IQueryable<Item> itemsQ = _context.Items
                                        .OrderBy(on => on.Id);

            if (qsParameters.Name != null && !qsParameters.Name.Trim().Equals(string.Empty))
                itemsQ = itemsQ.Where(item => item.Name.ToLower().Contains(qsParameters.Name.Trim().ToLower()));

            if (qsParameters.Category != null && !qsParameters.Category.Trim().Equals(string.Empty))
            {
                string[] categories = qsParameters.Category.Split('#');
                itemsQ = itemsQ.Where(item => categories.Contains(item.Category));
            }

            //get total count before paging
            int count = await itemsQ.CountAsync();

            itemsQ = itemsQ.Skip((qsParameters.PageNumber - 1) * qsParameters.PageSize)
                                        .Take(qsParameters.PageSize);

            List<Item> list = await itemsQ.ToListAsync();
            
            return new ItemPayload(list, count);
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var Item = await _context.Items.FindAsync(id);

            if (Item == null)
            {
                return NotFound();
            }

            return Item;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var Item = await _context.Items.FindAsync(id);

            if (Item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(Item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}