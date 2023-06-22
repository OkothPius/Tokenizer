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
    public class FundApplicationsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public FundApplicationsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/FundApplications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FundApplication>>> GetFundApplications()
        {
          if (_context.FundApplications == null)
          {
              return NotFound();
          }
            return await _context.FundApplications.ToListAsync();
        }

        // GET: api/FundApplications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FundApplication>> GetFundApplication(int id)
        {
          if (_context.FundApplications == null)
          {
              return NotFound();
          }
            var fundApplication = await _context.FundApplications.FindAsync(id);

            if (fundApplication == null)
            {
                return NotFound();
            }

            return fundApplication;
        }

        // PUT: api/FundApplications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFundApplication(int id, FundApplication fundApplication)
        {
            if (id != fundApplication.FundApplicationId)
            {
                return BadRequest();
            }

            _context.Entry(fundApplication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FundApplicationExists(id))
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

        // POST: api/FundApplications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FundApplication>> PostFundApplication(FundApplication fundApplication)
        {
          if (_context.FundApplications == null)
          {
              return Problem("Entity set 'DatabaseContext.FundApplications'  is null.");
          }
            _context.FundApplications.Add(fundApplication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFundApplication", new { id = fundApplication.FundApplicationId }, fundApplication);
        }

        // DELETE: api/FundApplications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFundApplication(int id)
        {
            if (_context.FundApplications == null)
            {
                return NotFound();
            }
            var fundApplication = await _context.FundApplications.FindAsync(id);
            if (fundApplication == null)
            {
                return NotFound();
            }

            _context.FundApplications.Remove(fundApplication);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FundApplicationExists(int id)
        {
            return (_context.FundApplications?.Any(e => e.FundApplicationId == id)).GetValueOrDefault();
        }
    }
}
