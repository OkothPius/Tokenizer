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
    [Route("api/[controller]")]
    [ApiController]
    public class FinanciersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public FinanciersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Financiers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Financier>>> GetFinanciers()
        {
          if (_context.Financiers == null)
          {
              return NotFound();
          }
            return await _context.Financiers.ToListAsync();
        }

        // GET: api/Financiers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Financier>> GetFinancier(int id)
        {
          if (_context.Financiers == null)
          {
              return NotFound();
          }
            var financier = await _context.Financiers.FindAsync(id);

            if (financier == null)
            {
                return NotFound();
            }

            return financier;
        }

        // PUT: api/Financiers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFinancier(int id, Financier financier)
        {
            if (id != financier.FinancierId)
            {
                return BadRequest();
            }

            _context.Entry(financier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinancierExists(id))
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

        // POST: api/Financiers
        [HttpPost]
        public async Task<ActionResult<Financier>> PostFinancier(Financier financier)
        {
          if (_context.Financiers == null)
          {
              return Problem("Entity set 'DatabaseContext.Financiers'  is null.");
          }
            _context.Financiers.Add(financier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFinancier", new { id = financier.FinancierId }, financier);
        }

        // DELETE: api/Financiers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinancier(int id)
        {
            if (_context.Financiers == null)
            {
                return NotFound();
            }
            var financier = await _context.Financiers.FindAsync(id);
            if (financier == null)
            {
                return NotFound();
            }

            _context.Financiers.Remove(financier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FinancierExists(int id)
        {
            return (_context.Financiers?.Any(e => e.FinancierId == id)).GetValueOrDefault();
        }
    }
}
