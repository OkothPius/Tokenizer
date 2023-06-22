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
    public class BankBranchesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public BankBranchesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/BankBranches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankBranch>>> GetBankBranches()
        {
          if (_context.BankBranches == null)
          {
              return NotFound();
          }
            return await _context.BankBranches.ToListAsync();
        }

        // GET: api/BankBranches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BankBranch>> GetBankBranch(int id)
        {
          if (_context.BankBranches == null)
          {
              return NotFound();
          }
            var bankBranch = await _context.BankBranches.FindAsync(id);

            if (bankBranch == null)
            {
                return NotFound();
            }

            return bankBranch;
        }

        // PUT: api/BankBranches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBankBranch(int id, BankBranch bankBranch)
        {
            if (id != bankBranch.BankBranchId)
            {
                return BadRequest();
            }

            _context.Entry(bankBranch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankBranchExists(id))
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

        // POST: api/BankBranches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BankBranch>> PostBankBranch(BankBranch bankBranch)
        {
          if (_context.BankBranches == null)
          {
              return Problem("Entity set 'DatabaseContext.BankBranches'  is null.");
          }
            _context.BankBranches.Add(bankBranch);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBankBranch", new { id = bankBranch.BankBranchId }, bankBranch);
        }

        // DELETE: api/BankBranches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankBranch(int id)
        {
            if (_context.BankBranches == null)
            {
                return NotFound();
            }
            var bankBranch = await _context.BankBranches.FindAsync(id);
            if (bankBranch == null)
            {
                return NotFound();
            }

            _context.BankBranches.Remove(bankBranch);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BankBranchExists(int id)
        {
            return (_context.BankBranches?.Any(e => e.BankBranchId == id)).GetValueOrDefault();
        }
    }
}
