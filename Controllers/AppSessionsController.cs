using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Models;

//using DAL_TestDB.Models;
//using DAL_TestDB.Data;
using WebAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AppSessionsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AppSessionsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/AppSessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppSession>>> GetAppSessions()
        {
          if (_context.AppSessions == null)
          {
              return NotFound();
          }
            return await _context.AppSessions.ToListAsync();
        }

        // GET: api/AppSessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppSession>> GetAppSession(int id)
        {
          if (_context.AppSessions == null)
          {
              return NotFound();
          }
            var appSession = await _context.AppSessions.FindAsync(id);

            if (appSession == null)
            {
                return NotFound();
            }

            return appSession;
        }

        // PUT: api/AppSessions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppSession(int id, AppSession appSession)
        {
            if (id != appSession.AppSessionId)
            {
                return BadRequest();
            }

            _context.Entry(appSession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppSessionExists(id))
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

        // POST: api/AppSessions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AppSession>> PostAppSession(AppSession appSession)
        {
          if (_context.AppSessions == null)
          {
              return Problem("Entity set 'DatabaseContext.AppSessions'  is null.");
          }
            _context.AppSessions.Add(appSession);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppSession", new { id = appSession.AppSessionId }, appSession);
        }

        // DELETE: api/AppSessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppSession(int id)
        {
            if (_context.AppSessions == null)
            {
                return NotFound();
            }
            var appSession = await _context.AppSessions.FindAsync(id);
            if (appSession == null)
            {
                return NotFound();
            }

            _context.AppSessions.Remove(appSession);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppSessionExists(int id)
        {
            return (_context.AppSessions?.Any(e => e.AppSessionId == id)).GetValueOrDefault();
        }
    }
}
