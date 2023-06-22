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
    public class RedeemsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public RedeemsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Redeems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Redeem>>> GetRedeems()
        {
          if (_context.Redeems == null)
          {
              return NotFound();
          }
            return await _context.Redeems.ToListAsync();
        }

        // GET: api/Redeems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Redeem>> GetRedeem(int id)
        {
          if (_context.Redeems == null)
          {
              return NotFound();
          }
            var redeem = await _context.Redeems.FindAsync(id);

            if (redeem == null)
            {
                return NotFound();
            }

            return redeem;
        }

        // PUT: api/Redeems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRedeem(int id, Redeem redeem)
        {
            if (id != redeem.RedeemId)
            {
                return BadRequest();
            }

            _context.Entry(redeem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RedeemExists(id))
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

        // POST: api/Redeems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Redeem>> PostRedeem(Redeem redeem)
        {
          if (_context.Redeems == null)
          {
              return Problem("Entity set 'DatabaseContext.Redeems'  is null.");
          }
            _context.Redeems.Add(redeem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRedeem", new { id = redeem.RedeemId }, redeem);
        }

        // DELETE: api/Redeems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRedeem(int id)
        {
            if (_context.Redeems == null)
            {
                return NotFound();
            }
            var redeem = await _context.Redeems.FindAsync(id);
            if (redeem == null)
            {
                return NotFound();
            }

            _context.Redeems.Remove(redeem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RedeemExists(int id)
        {
            return (_context.Redeems?.Any(e => e.RedeemId == id)).GetValueOrDefault();
        }
    }
}
