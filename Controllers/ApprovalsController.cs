﻿using System;
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
    public class ApprovalsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ApprovalsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Approvals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Approval>>> GetApprovals()
        {
          if (_context.Approvals == null)
          {
              return NotFound();
          }
            return await _context.Approvals.ToListAsync();
        }

        // GET: api/Approvals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Approval>> GetApproval(int id)
        {
          if (_context.Approvals == null)
          {
              return NotFound();
          }
            var approval = await _context.Approvals.FindAsync(id);

            if (approval == null)
            {
                return NotFound();
            }

            return approval;
        }

        // PUT: api/Approvals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApproval(int id, Approval approval)
        {
            if (id != approval.ApprovalId)
            {
                return BadRequest();
            }

            _context.Entry(approval).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApprovalExists(id))
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

        // POST: api/Approvals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Approval>> PostApproval(Approval approval)
        {
          if (_context.Approvals == null)
          {
              return Problem("Entity set 'DatabaseContext.Approvals'  is null.");
          }
            _context.Approvals.Add(approval);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApproval", new { id = approval.ApprovalId }, approval);
        }

        // DELETE: api/Approvals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApproval(int id)
        {
            if (_context.Approvals == null)
            {
                return NotFound();
            }
            var approval = await _context.Approvals.FindAsync(id);
            if (approval == null)
            {
                return NotFound();
            }

            _context.Approvals.Remove(approval);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApprovalExists(int id)
        {
            return (_context.Approvals?.Any(e => e.ApprovalId == id)).GetValueOrDefault();
        }
    }
}
