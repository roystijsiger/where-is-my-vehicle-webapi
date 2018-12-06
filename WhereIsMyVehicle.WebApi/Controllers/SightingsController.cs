using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhereIsMyVehicle.WebApi.Models;

namespace WhereIsMyVehicle.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class SightingsController : ControllerBase
    {
        private readonly WhereIsMyVehicleDbContext _context;

        public SightingsController(WhereIsMyVehicleDbContext context)
        {
            _context = context;
        }

        // GET: api/Sightings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sighting>>> GetSighting()
        {
            return await _context.Sighting.ToListAsync();
        }

        // GET: api/Sightings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sighting>> GetSighting(int id)
        {
            var sighting = await _context.Sighting.FindAsync(id);

            if (sighting == null)
            {
                return NotFound();
            }

            return sighting;
        }

        // PUT: api/Sightings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSighting(int id, Sighting sighting)
        {
            if (id != sighting.Id)
            {
                return BadRequest();
            }

            _context.Entry(sighting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SightingExists(id))
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

        // POST: api/Sightings
        [HttpPost]
        public async Task<ActionResult<Sighting>> PostSighting(Sighting sighting)
        {
            _context.Sighting.Add(sighting);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSighting", new { id = sighting.Id }, sighting);
        }

        // DELETE: api/Sightings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sighting>> DeleteSighting(int id)
        {
            var sighting = await _context.Sighting.FindAsync(id);
            if (sighting == null)
            {
                return NotFound();
            }

            _context.Sighting.Remove(sighting);
            await _context.SaveChangesAsync();

            return sighting;
        }

        private bool SightingExists(int id)
        {
            return _context.Sighting.Any(e => e.Id == id);
        }
    }
}
