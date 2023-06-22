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
    public class FundManagerUsersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public FundManagerUsersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/FundManagerUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FundManagerUser>>> GetFundManagerUsers()
        {
          if (_context.FundManagerUsers == null)
          {
              return NotFound();
          }
            return await _context.FundManagerUsers.ToListAsync();
        }

        // GET: api/FundManagerUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FundManagerUser>> GetFundManagerUser(int id)
        {
          if (_context.FundManagerUsers == null)
          {
              return NotFound();
          }
            var fundManagerUser = await _context.FundManagerUsers.FindAsync(id);

            if (fundManagerUser == null)
            {
                return NotFound();
            }

            return fundManagerUser;
        }

        // PUT: api/FundManagerUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFundManagerUser(int id, FundManagerUser fundManagerUser)
        {
            if (id != fundManagerUser.FundManagerUserId)
            {
                return BadRequest();
            }

            _context.Entry(fundManagerUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FundManagerUserExists(id))
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

        // POST: api/FundManagerUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FundManagerUser>> PostFundManagerUser(FundManagerUser fundManagerUser)
        {
          if (_context.FundManagerUsers == null)
          {
              return Problem("Entity set 'DatabaseContext.FundManagerUsers'  is null.");
          }
            _context.FundManagerUsers.Add(fundManagerUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFundManagerUser", new { id = fundManagerUser.FundManagerUserId }, fundManagerUser);
        }

        // DELETE: api/FundManagerUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFundManagerUser(int id)
        {
            if (_context.FundManagerUsers == null)
            {
                return NotFound();
            }
            var fundManagerUser = await _context.FundManagerUsers.FindAsync(id);
            if (fundManagerUser == null)
            {
                return NotFound();
            }

            _context.FundManagerUsers.Remove(fundManagerUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FundManagerUserExists(int id)
        {
            return (_context.FundManagerUsers?.Any(e => e.FundManagerUserId == id)).GetValueOrDefault();
        }
    }
}
