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
    public class VoucherTypesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public VoucherTypesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/VoucherTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoucherType>>> GetVoucherTypes()
        {
          if (_context.VoucherTypes == null)
          {
              return NotFound();
          }
            return await _context.VoucherTypes.ToListAsync();
        }

        // GET: api/VoucherTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VoucherType>> GetVoucherType(int id)
        {
          if (_context.VoucherTypes == null)
          {
              return NotFound();
          }
            var voucherType = await _context.VoucherTypes.FindAsync(id);

            if (voucherType == null)
            {
                return NotFound();
            }

            return voucherType;
        }

        // PUT: api/VoucherTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoucherType(int id, VoucherType voucherType)
        {
            if (id != voucherType.VoucherTypeId)
            {
                return BadRequest();
            }

            _context.Entry(voucherType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoucherTypeExists(id))
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

        // POST: api/VoucherTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VoucherType>> PostVoucherType(VoucherType voucherType)
        {
          if (_context.VoucherTypes == null)
          {
              return Problem("Entity set 'DatabaseContext.VoucherTypes'  is null.");
          }
            _context.VoucherTypes.Add(voucherType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoucherType", new { id = voucherType.VoucherTypeId }, voucherType);
        }

        // DELETE: api/VoucherTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoucherType(int id)
        {
            if (_context.VoucherTypes == null)
            {
                return NotFound();
            }
            var voucherType = await _context.VoucherTypes.FindAsync(id);
            if (voucherType == null)
            {
                return NotFound();
            }

            _context.VoucherTypes.Remove(voucherType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VoucherTypeExists(int id)
        {
            return (_context.VoucherTypes?.Any(e => e.VoucherTypeId == id)).GetValueOrDefault();
        }
    }
}
