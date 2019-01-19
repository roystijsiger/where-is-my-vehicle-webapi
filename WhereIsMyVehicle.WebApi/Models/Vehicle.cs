using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WhereIsMyVehicle.WebApi.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public VehicleType Type { get; set; }

        public string Brand { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }

        public double LastKnownLatitude { get; set; }
        public double LastKnownLongitude { get; set; }

        public virtual List<Sighting> Sightings { get; set; }
    }
}
