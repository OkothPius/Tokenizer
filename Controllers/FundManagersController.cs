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
    public class FundManagersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public FundManagersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/FundManagers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FundManager>>> GetFundManagers()
        {
          if (_context.FundManagers == null)
          {
              return NotFound();
          }
            return await _context.FundManagers.ToListAsync();
        }

        // GET: api/FundManagers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FundManager>> GetFundManager(int id)
        {
          if (_context.FundManagers == null)
          {
              return NotFound();
          }
            var fundManager = await _context.FundManagers.FindAsync(id);

            if (fundManager == null)
            {
                return NotFound();
            }

            return fundManager;
        }

        // PUT: api/FundManagers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFundManager(int id, FundManager fundManager)
        {
            if (id != fundManager.FundManagerId)
            {
                return BadRequest();
            }

            _context.Entry(fundManager).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FundManagerExists(id))
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

        // POST: api/FundManagers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FundManager>> PostFundManager(FundManager fundManager)
        {
          if (_context.FundManagers == null)
          {
              return Problem("Entity set 'DatabaseContext.FundManagers'  is null.");
          }
            _context.FundManagers.Add(fundManager);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFundManager", new { id = fundManager.FundManagerId }, fundManager);
        }

        // DELETE: api/FundManagers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFundManager(int id)
        {
            if (_context.FundManagers == null)
            {
                return NotFound();
            }
            var fundManager = await _context.FundManagers.FindAsync(id);
            if (fundManager == null)
            {
                return NotFound();
            }

            _context.FundManagers.Remove(fundManager);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FundManagerExists(int id)
        {
            return (_context.FundManagers?.Any(e => e.FundManagerId == id)).GetValueOrDefault();
        }
    }
}
