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
    public class SupplierApplicationsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SupplierApplicationsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/SupplierApplications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierApplication>>> GetSupplierApplications()
        {
          if (_context.SupplierApplications == null)
          {
              return NotFound();
          }
            return await _context.SupplierApplications.ToListAsync();
        }

        // GET: api/SupplierApplications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierApplication>> GetSupplierApplication(int id)
        {
          if (_context.SupplierApplications == null)
          {
              return NotFound();
          }
            var supplierApplication = await _context.SupplierApplications.FindAsync(id);

            if (supplierApplication == null)
            {
                return NotFound();
            }

            return supplierApplication;
        }

        // PUT: api/SupplierApplications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplierApplication(int id, SupplierApplication supplierApplication)
        {
            if (id != supplierApplication.SupplierApplicationId)
            {
                return BadRequest();
            }

            _context.Entry(supplierApplication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierApplicationExists(id))
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

        // POST: api/SupplierApplications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SupplierApplication>> PostSupplierApplication(SupplierApplication supplierApplication)
        {
          if (_context.SupplierApplications == null)
          {
              return Problem("Entity set 'DatabaseContext.SupplierApplications'  is null.");
          }
            _context.SupplierApplications.Add(supplierApplication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupplierApplication", new { id = supplierApplication.SupplierApplicationId }, supplierApplication);
        }

        // DELETE: api/SupplierApplications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplierApplication(int id)
        {
            if (_context.SupplierApplications == null)
            {
                return NotFound();
            }
            var supplierApplication = await _context.SupplierApplications.FindAsync(id);
            if (supplierApplication == null)
            {
                return NotFound();
            }

            _context.SupplierApplications.Remove(supplierApplication);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupplierApplicationExists(int id)
        {
            return (_context.SupplierApplications?.Any(e => e.SupplierApplicationId == id)).GetValueOrDefault();
        }
    }
}
