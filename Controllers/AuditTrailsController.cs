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
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuditTrailsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AuditTrailsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/AuditTrails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditTrail>>> GetAuditTrails()
        {
          if (_context.AuditTrails == null)
          {
              return NotFound();
          }
            return await _context.AuditTrails.ToListAsync();
        }

        // GET: api/AuditTrails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuditTrail>> GetAuditTrail(int id)
        {
          if (_context.AuditTrails == null)
          {
              return NotFound();
          }
            var auditTrail = await _context.AuditTrails.FindAsync(id);

            if (auditTrail == null)
            {
                return NotFound();
            }

            return auditTrail;
        }

        // PUT: api/AuditTrails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuditTrail(int id, AuditTrail auditTrail)
        {
            if (id != auditTrail.AuditTrailId)
            {
                return BadRequest();
            }

            _context.Entry(auditTrail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditTrailExists(id))
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

        // POST: api/AuditTrails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuditTrail>> PostAuditTrail(AuditTrail auditTrail)
        {
          if (_context.AuditTrails == null)
          {
              return Problem("Entity set 'DatabaseContext.AuditTrails'  is null.");
          }
            _context.AuditTrails.Add(auditTrail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuditTrail", new { id = auditTrail.AuditTrailId }, auditTrail);
        }

        // DELETE: api/AuditTrails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuditTrail(int id)
        {
            if (_context.AuditTrails == null)
            {
                return NotFound();
            }
            var auditTrail = await _context.AuditTrails.FindAsync(id);
            if (auditTrail == null)
            {
                return NotFound();
            }

            _context.AuditTrails.Remove(auditTrail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuditTrailExists(int id)
        {
            return (_context.AuditTrails?.Any(e => e.AuditTrailId == id)).GetValueOrDefault();
        }
    }
}
