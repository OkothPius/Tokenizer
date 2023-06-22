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
    public class BeneficiaryCategoriesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public BeneficiaryCategoriesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/BeneficiaryCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BeneficiaryCategory>>> GetBeneficiaryCategories()
        {
          if (_context.BeneficiaryCategories == null)
          {
              return NotFound();
          }
            return await _context.BeneficiaryCategories.ToListAsync();
        }

        // GET: api/BeneficiaryCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BeneficiaryCategory>> GetBeneficiaryCategory(int id)
        {
          if (_context.BeneficiaryCategories == null)
          {
              return NotFound();
          }
            var beneficiaryCategory = await _context.BeneficiaryCategories.FindAsync(id);

            if (beneficiaryCategory == null)
            {
                return NotFound();
            }

            return beneficiaryCategory;
        }

        // PUT: api/BeneficiaryCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeneficiaryCategory(int id, BeneficiaryCategory beneficiaryCategory)
        {
            if (id != beneficiaryCategory.BeneficiaryCategoryId)
            {
                return BadRequest();
            }

            _context.Entry(beneficiaryCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeneficiaryCategoryExists(id))
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

        // POST: api/BeneficiaryCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BeneficiaryCategory>> PostBeneficiaryCategory(BeneficiaryCategory beneficiaryCategory)
        {
          if (_context.BeneficiaryCategories == null)
          {
              return Problem("Entity set 'DatabaseContext.BeneficiaryCategories'  is null.");
          }
            _context.BeneficiaryCategories.Add(beneficiaryCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBeneficiaryCategory", new { id = beneficiaryCategory.BeneficiaryCategoryId }, beneficiaryCategory);
        }

        // DELETE: api/BeneficiaryCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeneficiaryCategory(int id)
        {
            if (_context.BeneficiaryCategories == null)
            {
                return NotFound();
            }
            var beneficiaryCategory = await _context.BeneficiaryCategories.FindAsync(id);
            if (beneficiaryCategory == null)
            {
                return NotFound();
            }

            _context.BeneficiaryCategories.Remove(beneficiaryCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeneficiaryCategoryExists(int id)
        {
            return (_context.BeneficiaryCategories?.Any(e => e.BeneficiaryCategoryId == id)).GetValueOrDefault();
        }
    }
}
