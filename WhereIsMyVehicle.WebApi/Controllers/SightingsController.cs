using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhereIsMyVehicle.WebApi.Data;
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
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Sighting>>> GetSightings()
        {
            return await _context.Sightings.ToListAsync();
        }
    }
}
