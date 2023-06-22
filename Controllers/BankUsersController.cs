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
    public class BankUsersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public BankUsersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/BankUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankUser>>> GetBankUsers()
        {
          if (_context.BankUsers == null)
          {
              return NotFound();
          }
            return await _context.BankUsers.ToListAsync();
        }

        // GET: api/BankUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BankUser>> GetBankUser(int id)
        {
          if (_context.BankUsers == null)
          {
              return NotFound();
          }
            var bankUser = await _context.BankUsers.FindAsync(id);

            if (bankUser == null)
            {
                return NotFound();
            }

            return bankUser;
        }

        // PUT: api/BankUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBankUser(int id, BankUser bankUser)
        {
            if (id != bankUser.BankUserId)
            {
                return BadRequest();
            }

            _context.Entry(bankUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankUserExists(id))
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

        // POST: api/BankUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BankUser>> PostBankUser(BankUser bankUser)
        {
          if (_context.BankUsers == null)
          {
              return Problem("Entity set 'DatabaseContext.BankUsers'  is null.");
          }
            _context.BankUsers.Add(bankUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBankUser", new { id = bankUser.BankUserId }, bankUser);
        }

        // DELETE: api/BankUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankUser(int id)
        {
            if (_context.BankUsers == null)
            {
                return NotFound();
            }
            var bankUser = await _context.BankUsers.FindAsync(id);
            if (bankUser == null)
            {
                return NotFound();
            }

            _context.BankUsers.Remove(bankUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BankUserExists(int id)
        {
            return (_context.BankUsers?.Any(e => e.BankUserId == id)).GetValueOrDefault();
        }
    }
}
