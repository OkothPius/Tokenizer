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
    public class VerifiesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public VerifiesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Verifies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Verify>>> GetVerifies()
        {
          if (_context.Verifies == null)
          {
              return NotFound();
          }
            return await _context.Verifies.ToListAsync();
        }

        // GET: api/Verifies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Verify>> GetVerify(int id)
        {
          if (_context.Verifies == null)
          {
              return NotFound();
          }
            var verify = await _context.Verifies.FindAsync(id);

            if (verify == null)
            {
                return NotFound();
            }

            return verify;
        }

        // PUT: api/Verifies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVerify(int id, Verify verify)
        {
            if (id != verify.VerifyId)
            {
                return BadRequest();
            }

            _context.Entry(verify).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerifyExists(id))
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

        // POST: api/Verifies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Verify>> PostVerify(Verify verify)
        {
          if (_context.Verifies == null)
          {
              return Problem("Entity set 'DatabaseContext.Verifies'  is null.");
          }
            _context.Verifies.Add(verify);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVerify", new { id = verify.VerifyId }, verify);
        }

        // DELETE: api/Verifies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVerify(int id)
        {
            if (_context.Verifies == null)
            {
                return NotFound();
            }
            var verify = await _context.Verifies.FindAsync(id);
            if (verify == null)
            {
                return NotFound();
            }

            _context.Verifies.Remove(verify);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VerifyExists(int id)
        {
            return (_context.Verifies?.Any(e => e.VerifyId == id)).GetValueOrDefault();
        }
    }
}
