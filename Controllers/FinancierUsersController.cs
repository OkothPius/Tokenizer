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

namespace API.Controllers
{
    //[JwtAuthorization]
    [Route("api/[controller]")]
    [ApiController]
    public class FinancierUsersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public FinancierUsersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/FinancierUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinancierUser>>> GetFinancierUsers()
        {
          if (_context.FinancierUsers == null)
          {
              return NotFound();
          }
            return await _context.FinancierUsers.ToListAsync();
        }

        // GET: api/FinancierUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FinancierUser>> GetFinancierUser(int id)
        {
          if (_context.FinancierUsers == null)
          {
              return NotFound();
          }
            var financierUser = await _context.FinancierUsers.FindAsync(id);

            if (financierUser == null)
            {
                return NotFound();
            }

            return financierUser;
        }

        // PUT: api/FinancierUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFinancierUser(int id, FinancierUser financierUser)
        {
            if (id != financierUser.FinancierUserId)
            {
                return BadRequest();
            }

            _context.Entry(financierUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinancierUserExists(id))
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

        // POST: api/FinancierUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FinancierUser>> PostFinancierUser(FinancierUser financierUser)
        {
          if (_context.FinancierUsers == null)
          {
              return Problem("Entity set 'DatabaseContext.FinancierUsers'  is null.");
          }
            _context.FinancierUsers.Add(financierUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFinancierUser", new { id = financierUser.FinancierUserId }, financierUser);
        }

        // DELETE: api/FinancierUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinancierUser(int id)
        {
            if (_context.FinancierUsers == null)
            {
                return NotFound();
            }
            var financierUser = await _context.FinancierUsers.FindAsync(id);
            if (financierUser == null)
            {
                return NotFound();
            }

            _context.FinancierUsers.Remove(financierUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FinancierUserExists(int id)
        {
            return (_context.FinancierUsers?.Any(e => e.FinancierUserId == id)).GetValueOrDefault();
        }
    }
}
