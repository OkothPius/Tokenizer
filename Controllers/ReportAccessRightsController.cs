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
    public class ReportAccessRightsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ReportAccessRightsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ReportAccessRights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportAccessRight>>> GetReportAccessRights()
        {
          if (_context.ReportAccessRights == null)
          {
              return NotFound();
          }
            return await _context.ReportAccessRights.ToListAsync();
        }

        // GET: api/ReportAccessRights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportAccessRight>> GetReportAccessRight(int id)
        {
          if (_context.ReportAccessRights == null)
          {
              return NotFound();
          }
            var reportAccessRight = await _context.ReportAccessRights.FindAsync(id);

            if (reportAccessRight == null)
            {
                return NotFound();
            }

            return reportAccessRight;
        }

        // PUT: api/ReportAccessRights/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportAccessRight(int id, ReportAccessRight reportAccessRight)
        {
            if (id != reportAccessRight.ReportAccessRightId)
            {
                return BadRequest();
            }

            _context.Entry(reportAccessRight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportAccessRightExists(id))
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

        // POST: api/ReportAccessRights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReportAccessRight>> PostReportAccessRight(ReportAccessRight reportAccessRight)
        {
          if (_context.ReportAccessRights == null)
          {
              return Problem("Entity set 'DatabaseContext.ReportAccessRights'  is null.");
          }
            _context.ReportAccessRights.Add(reportAccessRight);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReportAccessRight", new { id = reportAccessRight.ReportAccessRightId }, reportAccessRight);
        }

        // DELETE: api/ReportAccessRights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportAccessRight(int id)
        {
            if (_context.ReportAccessRights == null)
            {
                return NotFound();
            }
            var reportAccessRight = await _context.ReportAccessRights.FindAsync(id);
            if (reportAccessRight == null)
            {
                return NotFound();
            }

            _context.ReportAccessRights.Remove(reportAccessRight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportAccessRightExists(int id)
        {
            return (_context.ReportAccessRights?.Any(e => e.ReportAccessRightId == id)).GetValueOrDefault();
        }
    }
}
