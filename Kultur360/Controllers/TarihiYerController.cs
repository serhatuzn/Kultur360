using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kultur360.Data;
using Kultur360.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kultur360.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarihiYerController : ControllerBase
    {
        private readonly Kultur360DbContext _context;

        public TarihiYerController(Kultur360DbContext context)
        {
            _context = context;
        }

        // GET: api/TarihiYer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarihiYer>>> GetTarihiYerler()
        {
            return await _context.TarihiYerler.ToListAsync();
        }

        // GET: api/TarihiYer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TarihiYer>> GetTarihiYer(int id)
        {
            var yer = await _context.TarihiYerler.FindAsync(id);
            if (yer == null)
                return NotFound();

            return yer;
        }

        // POST: api/TarihiYer
        [HttpPost]
        public async Task<ActionResult<TarihiYer>> PostTarihiYer(TarihiYer yer)
        {
            _context.TarihiYerler.Add(yer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTarihiYer), new { id = yer.Id }, yer);
        }

        // PUT: api/TarihiYer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarihiYer(int id, TarihiYer yer)
        {
            if (id != yer.Id)
                return BadRequest();

            _context.Entry(yer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TarihiYerler.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/TarihiYer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarihiYer(int id)
        {
            var yer = await _context.TarihiYerler.FindAsync(id);
            if (yer == null)
                return NotFound();

            _context.TarihiYerler.Remove(yer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}