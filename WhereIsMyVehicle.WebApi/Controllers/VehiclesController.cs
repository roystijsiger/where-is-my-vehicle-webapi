using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            return await _context.Vehicles.Include(v => v.User).ToListAsync();
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<Vehicle> GetVehicle(int id)
        {
            var vehicle = _context.Vehicles.Include(v => v.User).SingleOrDefault(v => v.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        // POST: api/Vehicles
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == GetCurrentUserEmail());

            if (user == null) return Unauthorized();

            vehicle.User = user;

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicle", new { id = vehicle.Id }, vehicle);
        }

   
        [AllowAnonymous]
        [HttpGet("{vehicleId}/sightings")]
        public ActionResult<IEnumerable<Sighting>> GetVehicleSightingsAsync(int vehicleId)
        {
            var vehicle = _context.Vehicles
                .Include(v => v.User)
                .Include(v => v.Sightings)
                .SingleOrDefault(v => v.Id == vehicleId);

            if (vehicle == null)
            {
                return BadRequest();
            }

            if (vehicle.User.Email != GetCurrentUserEmail())
            {
                return Unauthorized();
            }

            return Ok(vehicle.Sightings);
        }

        // GET: api/vehicles/1/sightings/5
        [HttpGet("{vehicleId}/sightings/{sightingId}")]
        [AllowAnonymous]
        public ActionResult<Sighting> GetVehicleSightingAsync(int vehicleId, int sightingId)
        {
            var vehicle = _context.Vehicles
                .Include(v => v.User)
                .Include(v => v.Sightings)
                .SingleOrDefault(v => v.Id == vehicleId);

            if (vehicle == null)
            {
                return BadRequest();
            }

            if (vehicle.User.Email != GetCurrentUserEmail())
            {
                return Unauthorized();
            }

            var sighting = vehicle.Sightings
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

            return StatusCode(201);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vehicle>> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            if (vehicle.User.Email != GetCurrentUserEmail())
            {
                return Unauthorized();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }

        private string GetCurrentUserEmail() => 
            HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    }
}
