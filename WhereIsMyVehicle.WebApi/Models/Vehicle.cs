using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereIsMyVehicle.WebApi.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public VehicleType Type { get; set; }
        public string Color { get; set; }
        public string LicencePlate { get; set; }
        public Location Location { get; set; }

        public List<Sighting> Sightings { get; set; }
    }
}
