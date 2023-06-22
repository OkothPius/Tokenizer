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
    public class BeneficiariesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public BeneficiariesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Beneficiaries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beneficiary>>> GetBeneficiaries()
        {
          if (_context.Beneficiaries == null)
          {
              return NotFound();
          }
            return await _context.Beneficiaries.ToListAsync();
        }

        // GET: api/Beneficiaries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Beneficiary>> GetBeneficiary(int id)
        {
          if (_context.Beneficiaries == null)
          {
              return NotFound();
          }
            var beneficiary = await _context.Beneficiaries.FindAsync(id);

            if (beneficiary == null)
            {
                return NotFound();
            }

            return beneficiary;
        }

        // PUT: api/Beneficiaries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeneficiary(int id, Beneficiary beneficiary)
        {
            if (id != beneficiary.BeneficiaryId)
            {
                return BadRequest();
            }

            _context.Entry(beneficiary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeneficiaryExists(id))
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

        // POST: api/Beneficiaries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Beneficiary>> PostBeneficiary(Beneficiary beneficiary)
        {
          if (_context.Beneficiaries == null)
          {
              return Problem("Entity set 'DatabaseContext.Beneficiaries'  is null.");
          }
            _context.Beneficiaries.Add(beneficiary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBeneficiary", new { id = beneficiary.BeneficiaryId }, beneficiary);
        }

        // DELETE: api/Beneficiaries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeneficiary(int id)
        {
            if (_context.Beneficiaries == null)
            {
                return NotFound();
            }
            var beneficiary = await _context.Beneficiaries.FindAsync(id);
            if (beneficiary == null)
            {
                return NotFound();
            }

            _context.Beneficiaries.Remove(beneficiary);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeneficiaryExists(int id)
        {
            return (_context.Beneficiaries?.Any(e => e.BeneficiaryId == id)).GetValueOrDefault();
        }
    }
}
