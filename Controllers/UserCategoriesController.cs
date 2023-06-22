using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace webAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserCategoriesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UserCategoriesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/UserCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCategory>>> GetUserCategories()
        {
            if (_context.UserCategories == null)
            {
                return NotFound();
            }
            return await _context.UserCategories.ToListAsync();
        }

        // GET: api/UserCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCategory>> GetUserCategory(int id)
        {
            if (_context.UserCategories == null)
            {
                return NotFound();
            }
            var userCategory = await _context.UserCategories.FindAsync(id);

            if (userCategory == null)
            {
                return NotFound();
            }

            return userCategory;
        }

        // PUT: api/UserCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCategory(int id, UserCategory userCategory)
        {
            if (id != userCategory.UserCategoryId)
            {
                return BadRequest();
            }

            _context.Entry(userCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCategoryExists(id))
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

        // POST: api/UserCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserCategory>> PostUserCategory(UserCategory userCategory)
        {
            if (_context.UserCategories == null)
            {
                return Problem("Entity set 'DatabaseContext.UserCategories'  is null.");
            }
            _context.UserCategories.Add(userCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserCategory", new { id = userCategory.UserCategoryId }, userCategory);
        }

        // DELETE: api/UserCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCategory(int id)
        {
            if (_context.UserCategories == null)
            {
                return NotFound();
            }
            var userCategory = await _context.UserCategories.FindAsync(id);
            if (userCategory == null)
            {
                return NotFound();
            }

            _context.UserCategories.Remove(userCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserCategoryExists(int id)
        {
            return (_context.UserCategories?.Any(e => e.UserCategoryId == id)).GetValueOrDefault();
        }
    }
}
