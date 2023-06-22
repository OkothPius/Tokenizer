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
    public class DisbursementModesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public DisbursementModesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/DisbursementModes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisbursementMode>>> GetDisbursementModes()
        {
          if (_context.DisbursementModes == null)
          {
              return NotFound();
          }
            return await _context.DisbursementModes.ToListAsync();
        }

        // GET: api/DisbursementModes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DisbursementMode>> GetDisbursementMode(int id)
        {
          if (_context.DisbursementModes == null)
          {
              return NotFound();
          }
            var disbursementMode = await _context.DisbursementModes.FindAsync(id);

            if (disbursementMode == null)
            {
                return NotFound();
            }

            return disbursementMode;
        }

        // PUT: api/DisbursementModes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDisbursementMode(int id, DisbursementMode disbursementMode)
        {
            if (id != disbursementMode.DisbursementModeId)
            {
                return BadRequest();
            }

            _context.Entry(disbursementMode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DisbursementModeExists(id))
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

        // POST: api/DisbursementModes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DisbursementMode>> PostDisbursementMode(DisbursementMode disbursementMode)
        {
          if (_context.DisbursementModes == null)
          {
              return Problem("Entity set 'DatabaseContext.DisbursementModes'  is null.");
          }
            _context.DisbursementModes.Add(disbursementMode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDisbursementMode", new { id = disbursementMode.DisbursementModeId }, disbursementMode);
        }

        // DELETE: api/DisbursementModes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisbursementMode(int id)
        {
            if (_context.DisbursementModes == null)
            {
                return NotFound();
            }
            var disbursementMode = await _context.DisbursementModes.FindAsync(id);
            if (disbursementMode == null)
            {
                return NotFound();
            }

            _context.DisbursementModes.Remove(disbursementMode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DisbursementModeExists(int id)
        {
            return (_context.DisbursementModes?.Any(e => e.DisbursementModeId == id)).GetValueOrDefault();
        }
    }
}
