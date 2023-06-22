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
    public class SupplierUsersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SupplierUsersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/SupplierUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierUser>>> GetSupplierUsers()
        {
          if (_context.SupplierUsers == null)
          {
              return NotFound();
          }
            return await _context.SupplierUsers.ToListAsync();
        }

        // GET: api/SupplierUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierUser>> GetSupplierUser(int id)
        {
          if (_context.SupplierUsers == null)
          {
              return NotFound();
          }
            var supplierUser = await _context.SupplierUsers.FindAsync(id);

            if (supplierUser == null)
            {
                return NotFound();
            }

            return supplierUser;
        }

        // PUT: api/SupplierUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplierUser(int id, SupplierUser supplierUser)
        {
            if (id != supplierUser.SupplierUserId)
            {
                return BadRequest();
            }

            _context.Entry(supplierUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierUserExists(id))
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

        // POST: api/SupplierUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SupplierUser>> PostSupplierUser(SupplierUser supplierUser)
        {
          if (_context.SupplierUsers == null)
          {
              return Problem("Entity set 'DatabaseContext.SupplierUsers'  is null.");
          }
            _context.SupplierUsers.Add(supplierUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupplierUser", new { id = supplierUser.SupplierUserId }, supplierUser);
        }

        // DELETE: api/SupplierUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplierUser(int id)
        {
            if (_context.SupplierUsers == null)
            {
                return NotFound();
            }
            var supplierUser = await _context.SupplierUsers.FindAsync(id);
            if (supplierUser == null)
            {
                return NotFound();
            }

            _context.SupplierUsers.Remove(supplierUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupplierUserExists(int id)
        {
            return (_context.SupplierUsers?.Any(e => e.SupplierUserId == id)).GetValueOrDefault();
        }
    }
}
