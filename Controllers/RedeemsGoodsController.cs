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
    public class RedeemsGoodsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public RedeemsGoodsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/RedeemsGoods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RedeemsGood>>> GetRedeemsGoods()
        {
          if (_context.RedeemsGoods == null)
          {
              return NotFound();
          }
            return await _context.RedeemsGoods.ToListAsync();
        }

        // GET: api/RedeemsGoods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RedeemsGood>> GetRedeemsGood(int id)
        {
          if (_context.RedeemsGoods == null)
          {
              return NotFound();
          }
            var redeemsGood = await _context.RedeemsGoods.FindAsync(id);

            if (redeemsGood == null)
            {
                return NotFound();
            }

            return redeemsGood;
        }

        // PUT: api/RedeemsGoods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRedeemsGood(int id, RedeemsGood redeemsGood)
        {
            if (id != redeemsGood.RedeemsGoodsId)
            {
                return BadRequest();
            }

            _context.Entry(redeemsGood).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RedeemsGoodExists(id))
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

        // POST: api/RedeemsGoods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RedeemsGood>> PostRedeemsGood(RedeemsGood redeemsGood)
        {
          if (_context.RedeemsGoods == null)
          {
              return Problem("Entity set 'DatabaseContext.RedeemsGoods'  is null.");
          }
            _context.RedeemsGoods.Add(redeemsGood);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRedeemsGood", new { id = redeemsGood.RedeemsGoodsId }, redeemsGood);
        }

        // DELETE: api/RedeemsGoods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRedeemsGood(int id)
        {
            if (_context.RedeemsGoods == null)
            {
                return NotFound();
            }
            var redeemsGood = await _context.RedeemsGoods.FindAsync(id);
            if (redeemsGood == null)
            {
                return NotFound();
            }

            _context.RedeemsGoods.Remove(redeemsGood);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RedeemsGoodExists(int id)
        {
            return (_context.RedeemsGoods?.Any(e => e.RedeemsGoodsId == id)).GetValueOrDefault();
        }
    }
}
