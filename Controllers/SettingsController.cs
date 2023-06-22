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

namespace API.Controllers
{
    //[JwtAuthorization]
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SettingsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Settings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Setting>>> GetSettings()
        {
          if (_context.Settings == null)
          {
              return NotFound();
          }
            return await _context.Settings.ToListAsync();
        }

        // GET: api/Settings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Setting>> GetSetting(int id)
        {
          if (_context.Settings == null)
          {
              return NotFound();
          }
            var setting = await _context.Settings.FindAsync(id);

            if (setting == null)
            {
                return NotFound();
            }

            return setting;
        }

        // PUT: api/Settings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSetting(int id, Setting setting)
        {
            if (id != setting.SettingId)
            {
                return BadRequest();
            }

            _context.Entry(setting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SettingExists(id))
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

        // POST: api/Settings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Setting>> PostSetting(Setting setting)
        {
          if (_context.Settings == null)
          {
              return Problem("Entity set 'DatabaseContext.Settings'  is null.");
          }
            _context.Settings.Add(setting);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSetting", new { id = setting.SettingId }, setting);
        }

        // DELETE: api/Settings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSetting(int id)
        {
            if (_context.Settings == null)
            {
                return NotFound();
            }
            var setting = await _context.Settings.FindAsync(id);
            if (setting == null)
            {
                return NotFound();
            }

            _context.Settings.Remove(setting);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SettingExists(int id)
        {
            return (_context.Settings?.Any(e => e.SettingId == id)).GetValueOrDefault();
        }
    }
}
