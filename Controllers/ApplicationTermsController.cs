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
    public class ApplicationTermsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ApplicationTermsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationTerms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationTerm>>> GetApplicationTerms()
        {
          if (_context.ApplicationTerms == null)
          {
              return NotFound();
          }
            return await _context.ApplicationTerms.ToListAsync();
        }

        // GET: api/ApplicationTerms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationTerm>> GetApplicationTerm(int id)
        {
          if (_context.ApplicationTerms == null)
          {
              return NotFound();
          }
            var applicationTerm = await _context.ApplicationTerms.FindAsync(id);

            if (applicationTerm == null)
            {
                return NotFound();
            }

            return applicationTerm;
        }

        // PUT: api/ApplicationTerms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationTerm(int id, ApplicationTerm applicationTerm)
        {
            if (id != applicationTerm.ApplicationTermsId)
            {
                return BadRequest();
            }

            _context.Entry(applicationTerm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationTermExists(id))
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

        // POST: api/ApplicationTerms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApplicationTerm>> PostApplicationTerm(ApplicationTerm applicationTerm)
        {
          if (_context.ApplicationTerms == null)
          {
              return Problem("Entity set 'DatabaseContext.ApplicationTerms'  is null.");
          }
            _context.ApplicationTerms.Add(applicationTerm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationTerm", new { id = applicationTerm.ApplicationTermsId }, applicationTerm);
        }

        // DELETE: api/ApplicationTerms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationTerm(int id)
        {
            if (_context.ApplicationTerms == null)
            {
                return NotFound();
            }
            var applicationTerm = await _context.ApplicationTerms.FindAsync(id);
            if (applicationTerm == null)
            {
                return NotFound();
            }

            _context.ApplicationTerms.Remove(applicationTerm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicationTermExists(int id)
        {
            return (_context.ApplicationTerms?.Any(e => e.ApplicationTermsId == id)).GetValueOrDefault();
        }
    }
}
