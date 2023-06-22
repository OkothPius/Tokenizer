using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Models;

//using DAL_TestDB.Models;
//using DAL_TestDB.Data;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessRightsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AccessRightsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/AccessRights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccessRight>>> GetAccessRights()
        {
          if (_context.AccessRights == null)
          {
              return NotFound();
          }
            return await _context.AccessRights.ToListAsync();
        }

        // GET: api/AccessRights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccessRight>> GetAccessRight(int id)
        {
          if (_context.AccessRights == null)
          {
              return NotFound();
          }
            var accessRight = await _context.AccessRights.FindAsync(id);

            if (accessRight == null)
            {
                return NotFound();
            }

            return accessRight;
        }

        // PUT: api/AccessRights/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccessRight(int id, AccessRight accessRight)
        {
            if (id != accessRight.AccessRightId)
            {
                return BadRequest();
            }

            _context.Entry(accessRight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessRightExists(id))
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

        // POST: api/AccessRights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccessRight>> PostAccessRight(AccessRight accessRight)
        {
          if (_context.AccessRights == null)
          {
              return Problem("Entity set 'DatabaseContext.AccessRights'  is null.");
          }
            _context.AccessRights.Add(accessRight);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccessRight", new { id = accessRight.AccessRightId }, accessRight);
        }

        // DELETE: api/AccessRights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccessRight(int id)
        {
            if (_context.AccessRights == null)
            {
                return NotFound();
            }
            var accessRight = await _context.AccessRights.FindAsync(id);
            if (accessRight == null)
            {
                return NotFound();
            }

            _context.AccessRights.Remove(accessRight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccessRightExists(int id)
        {
            return (_context.AccessRights?.Any(e => e.AccessRightId == id)).GetValueOrDefault();
        }
    }
}
