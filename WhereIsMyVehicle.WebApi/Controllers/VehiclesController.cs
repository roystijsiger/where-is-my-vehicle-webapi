using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhereIsMyVehicle.WebApi.Data;
using WhereIsMyVehicle.WebApi.Helpers;
using WhereIsMyVehicle.WebApi.Models;

namespace WhereIsMyVehicle.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class VehiclesController : ControllerBase
    {
        private readonly WhereIsMyVehicleDbContext _context;

        public VehiclesController(WhereIsMyVehicleDbContext context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles([FromQuery] VehicleFilters filters)
        {
            return await _context.Vehicles.ToListAsync();
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        // PUT: api/Vehicles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
                {
                    return NotFound();
                }
             
                throw;
                
            }

            return NoContent();
        }

        // POST: api/Vehicles
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicle", new { id = vehicle.Id }, vehicle);
        }

   
        [AllowAnonymous]
        [HttpGet("{vehicleId}/sightings")]
        public ActionResult<IEnumerable<Sighting>> GetVehicleSightings(int vehicleId)
        {
            return _context.Vehicles
                .SingleOrDefault(v => v.Id == vehicleId)?
                .Sightings;
        }

        // GET: api/vehicles/1/sightings/5
        [HttpGet("{vehicleId}/sightings/{sightingId}")]
        [AllowAnonymous]
        public ActionResult<Sighting> GetVehicleSighting(int vehicleId, int sightingId)
        {
            var sighting = _context.Vehicles
                .SingleOrDefault(v => v.Id == vehicleId)?
                .Sightings
                .SingleOrDefault(s => s.Id == sightingId);

            if (sighting == null)
            {
                return NotFound();
            }

            return sighting;
        }

        // POST: api/vehicles/1/sightings
        [HttpPost("{vehicleId}/sightings")]
        [AllowAnonymous]
        public async Task<ActionResult<Sighting>> PostVehicleSighting(int vehicleId, Sighting sighting)
        {
            var vehicle = _context.Vehicles.SingleOrDefault(v => v.Id == vehicleId);

            if (vehicle == null)
            {
                return BadRequest("No vehicle available with the provided id");
            }

            if (vehicle.Sightings == null)
            {
                vehicle.Sightings = new List<Sighting>();
            }

            vehicle.Sightings.Add(sighting);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleSighting", new { vehicleId = vehicle.Id, sightingId = sighting.Id }, sighting);
        }

        // DELETE: api/vehicles/1/sightings/5
//        [HttpDelete("{vehicleId}/sightings/{sightingId}")]
//        public async Task<ActionResult<Sighting>> DeleteVehicleSighting(int vehicleId, int sightingId)
//        {
//            var vehicle = _context.Vehicles.SingleOrDefault(v => v.Id == vehicleId);
//
//            if (vehicle == null)
//            {
//                return BadRequest("No vehicle available with the provided id");
//            }
//
//            var sighting = vehicle.Sightings.Find(s => s.Id == sightingId);
//            if (sighting == null)
//            {
//                return NotFound();
//            }
//
//            _context.Sightings.Remove(sighting);
//            await _context.SaveChangesAsync();
//
//            return sighting;
//        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vehicle>> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
}
