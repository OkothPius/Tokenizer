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
    public class ApplicationAttachmentsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ApplicationAttachmentsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationAttachments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationAttachment>>> GetApplicationAttachments()
        {
          if (_context.ApplicationAttachments == null)
          {
              return NotFound();
          }
            return await _context.ApplicationAttachments.ToListAsync();
        }

        // GET: api/ApplicationAttachments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationAttachment>> GetApplicationAttachment(int id)
        {
          if (_context.ApplicationAttachments == null)
          {
              return NotFound();
          }
            var applicationAttachment = await _context.ApplicationAttachments.FindAsync(id);

            if (applicationAttachment == null)
            {
                return NotFound();
            }

            return applicationAttachment;
        }

        // PUT: api/ApplicationAttachments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationAttachment(int id, ApplicationAttachment applicationAttachment)
        {
            if (id != applicationAttachment.ApplicationAttachementId)
            {
                return BadRequest();
            }

            _context.Entry(applicationAttachment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationAttachmentExists(id))
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

        // POST: api/ApplicationAttachments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApplicationAttachment>> PostApplicationAttachment(ApplicationAttachment applicationAttachment)
        {
          if (_context.ApplicationAttachments == null)
          {
              return Problem("Entity set 'DatabaseContext.ApplicationAttachments'  is null.");
          }
            _context.ApplicationAttachments.Add(applicationAttachment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationAttachment", new { id = applicationAttachment.ApplicationAttachementId }, applicationAttachment);
        }

        // DELETE: api/ApplicationAttachments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationAttachment(int id)
        {
            if (_context.ApplicationAttachments == null)
            {
                return NotFound();
            }
            var applicationAttachment = await _context.ApplicationAttachments.FindAsync(id);
            if (applicationAttachment == null)
            {
                return NotFound();
            }

            _context.ApplicationAttachments.Remove(applicationAttachment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicationAttachmentExists(int id)
        {
            return (_context.ApplicationAttachments?.Any(e => e.ApplicationAttachementId == id)).GetValueOrDefault();
        }
    }
}
