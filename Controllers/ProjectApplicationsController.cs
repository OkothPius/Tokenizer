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

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectApplicationsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ProjectApplicationsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ProjectApplications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectApplication>>> GetProjectApplications()
        {
          if (_context.ProjectApplications == null)
          {
              return NotFound();
          }
            return await _context.ProjectApplications.ToListAsync();
        }

        // GET: api/ProjectApplications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectApplication>> GetProjectApplication(int id)
        {
          if (_context.ProjectApplications == null)
          {
              return NotFound();
          }
            var projectApplication = await _context.ProjectApplications.FindAsync(id);

            if (projectApplication == null)
            {
                return NotFound();
            }

            return projectApplication;
        }

        // PUT: api/ProjectApplications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectApplication(int id, ProjectApplication projectApplication)
        {
            if (id != projectApplication.ProjectApplicationId)
            {
                return BadRequest();
            }

            _context.Entry(projectApplication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectApplicationExists(id))
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

        // POST: api/ProjectApplications
        [HttpPost]
        public async Task<ActionResult<ProjectApplication>> PostProjectApplication(ProjectApplication projectApplication)
        {
          if (_context.ProjectApplications == null)
          {
              return Problem("Entity set 'DatabaseContext.ProjectApplications'  is null.");
          }
            _context.ProjectApplications.Add(projectApplication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectApplication", new { id = projectApplication.ProjectApplicationId }, projectApplication);
        }

        // DELETE: api/ProjectApplications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectApplication(int id)
        {
            if (_context.ProjectApplications == null)
            {
                return NotFound();
            }
            var projectApplication = await _context.ProjectApplications.FindAsync(id);
            if (projectApplication == null)
            {
                return NotFound();
            }

            _context.ProjectApplications.Remove(projectApplication);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectApplicationExists(int id)
        {
            return (_context.ProjectApplications?.Any(e => e.ProjectApplicationId == id)).GetValueOrDefault();
        }
    }
}
