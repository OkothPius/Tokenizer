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
    public class FundPurposesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public FundPurposesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/FundPurposes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FundPurpose>>> GetFundPurposes()
        {
          if (_context.FundPurposes == null)
          {
              return NotFound();
          }
            return await _context.FundPurposes.ToListAsync();
        }

        // GET: api/FundPurposes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FundPurpose>> GetFundPurpose(int id)
        {
          if (_context.FundPurposes == null)
          {
              return NotFound();
          }
            var fundPurpose = await _context.FundPurposes.FindAsync(id);

            if (fundPurpose == null)
            {
                return NotFound();
            }

            return fundPurpose;
        }

        // PUT: api/FundPurposes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFundPurpose(int id, FundPurpose fundPurpose)
        {
            if (id != fundPurpose.FundPurposeId)
            {
                return BadRequest();
            }

            _context.Entry(fundPurpose).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FundPurposeExists(id))
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

        // POST: api/FundPurposes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FundPurpose>> PostFundPurpose(FundPurpose fundPurpose)
        {
          if (_context.FundPurposes == null)
          {
              return Problem("Entity set 'DatabaseContext.FundPurposes'  is null.");
          }
            _context.FundPurposes.Add(fundPurpose);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFundPurpose", new { id = fundPurpose.FundPurposeId }, fundPurpose);
        }

        // DELETE: api/FundPurposes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFundPurpose(int id)
        {
            if (_context.FundPurposes == null)
            {
                return NotFound();
            }
            var fundPurpose = await _context.FundPurposes.FindAsync(id);
            if (fundPurpose == null)
            {
                return NotFound();
            }

            _context.FundPurposes.Remove(fundPurpose);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FundPurposeExists(int id)
        {
            return (_context.FundPurposes?.Any(e => e.FundPurposeId == id)).GetValueOrDefault();
        }
    }
}
