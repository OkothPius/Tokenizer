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
    public class FundsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public FundsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Funds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fund>>> GetFunds()
        {
          if (_context.Funds == null)
          {
              return NotFound();
          }
            return await _context.Funds.ToListAsync();
        }

        // GET: api/Funds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fund>> GetFund(int id)
        {
          if (_context.Funds == null)
          {
              return NotFound();
          }
            var fund = await _context.Funds.FindAsync(id);

            if (fund == null)
            {
                return NotFound();
            }

            return fund;
        }

        // PUT: api/Funds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFund(int id, Fund fund)
        {
            if (id != fund.FundId)
            {
                return BadRequest();
            }

            _context.Entry(fund).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FundExists(id))
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

        // POST: api/Funds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fund>> PostFund(Fund fund)
        {
          if (_context.Funds == null)
          {
              return Problem("Entity set 'DatabaseContext.Funds'  is null.");
          }
            _context.Funds.Add(fund);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFund", new { id = fund.FundId }, fund);
        }

        // DELETE: api/Funds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFund(int id)
        {
            if (_context.Funds == null)
            {
                return NotFound();
            }
            var fund = await _context.Funds.FindAsync(id);
            if (fund == null)
            {
                return NotFound();
            }

            _context.Funds.Remove(fund);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FundExists(int id)
        {
            return (_context.Funds?.Any(e => e.FundId == id)).GetValueOrDefault();
        }
    }
}
