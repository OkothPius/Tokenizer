using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Models;
using DAL;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        protected readonly JsonSerializerOptions _jsonSerializerOptions;

        public MenuItemsController(DatabaseContext context)
        {
            _context = context;

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 64 // Increase the maximum allowed depth if needed
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenuItems()
        {
            if (_context.MenuItems == null)
            {
                return NotFound();
            }

            var json = JsonSerializer.Serialize(_context.MenuItems, _jsonSerializerOptions);
            var result = new ContentResult
            {
                Content = json,
                ContentType = "application/json",
                StatusCode = 200
            };

            return result;
        }


        // GET: api/MenuItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> GetMenuItem(int id)
        {
          if (_context.MenuItems == null)
          {
              return NotFound();
          }
            var menuItem = await _context.MenuItems.FindAsync(id);

            if (menuItem == null)
            {
                return NotFound();
            }

            return menuItem;
        }

        // PUT: api/MenuItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuItem(int id, MenuItem menuItem)
        {
            if (id != menuItem.MenuItemId)
            {
                return BadRequest();
            }

            _context.Entry(menuItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MenuItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MenuItem>> PostMenuItem(MenuItem menuItem)
        {
          if (_context.MenuItems == null)
          {
              return Problem("Entity set 'DatabaseContext.MenuItems'  is null.");
          }
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenuItem", new { id = menuItem.MenuItemId }, menuItem);
        }

        // DELETE: api/MenuItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            if (_context.MenuItems == null)
            {
                return NotFound();
            }
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }

            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuItemExists(int id)
        {
            return (_context.MenuItems?.Any(e => e.MenuItemId == id)).GetValueOrDefault();
        }
    }
}
