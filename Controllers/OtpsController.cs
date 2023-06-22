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
    public class OtpsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public OtpsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Otps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Otp>>> GetOtps()
        {
          if (_context.Otps == null)
          {
              return NotFound();
          }
            return await _context.Otps.ToListAsync();
        }

        // GET: api/Otps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Otp>> GetOtp(int id)
        {
          if (_context.Otps == null)
          {
              return NotFound();
          }
            var otp = await _context.Otps.FindAsync(id);

            if (otp == null)
            {
                return NotFound();
            }

            return otp;
        }

        // PUT: api/Otps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOtp(int id, Otp otp)
        {
            if (id != otp.Otpid)
            {
                return BadRequest();
            }

            _context.Entry(otp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OtpExists(id))
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

        // POST: api/Otps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Otp>> PostOtp(Otp otp)
        {
          if (_context.Otps == null)
          {
              return Problem("Entity set 'DatabaseContext.Otps'  is null.");
          }
            _context.Otps.Add(otp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOtp", new { id = otp.Otpid }, otp);
        }

        // DELETE: api/Otps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOtp(int id)
        {
            if (_context.Otps == null)
            {
                return NotFound();
            }
            var otp = await _context.Otps.FindAsync(id);
            if (otp == null)
            {
                return NotFound();
            }

            _context.Otps.Remove(otp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OtpExists(int id)
        {
            return (_context.Otps?.Any(e => e.Otpid == id)).GetValueOrDefault();
        }
    }
}
