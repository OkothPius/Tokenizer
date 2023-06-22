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
    public class CurrenciesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CurrenciesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Currencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencies()
        {
          if (_context.Currencies == null)
          {
              return NotFound();
          }
            return await _context.Currencies.ToListAsync();
        }

        // GET: api/Currencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Currency>> GetCurrency(int id)
        {
          if (_context.Currencies == null)
          {
              return NotFound();
          }
            var currency = await _context.Currencies.FindAsync(id);

            if (currency == null)
            {
                return NotFound();
            }

            return currency;
        }

        // PUT: api/Currencies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurrency(int id, Currency currency)
        {
            if (id != currency.CurrencyId)
            {
                return BadRequest();
            }

            _context.Entry(currency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyExists(id))
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

        // POST: api/Currencies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Currency>> PostCurrency(Currency currency)
        {
          if (_context.Currencies == null)
          {
              return Problem("Entity set 'DatabaseContext.Currencies'  is null.");
          }
            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurrency", new { id = currency.CurrencyId }, currency);
        }

        // DELETE: api/Currencies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            if (_context.Currencies == null)
            {
                return NotFound();
            }
            var currency = await _context.Currencies.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }

            _context.Currencies.Remove(currency);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CurrencyExists(int id)
        {
            return (_context.Currencies?.Any(e => e.CurrencyId == id)).GetValueOrDefault();
        }
    }
}
