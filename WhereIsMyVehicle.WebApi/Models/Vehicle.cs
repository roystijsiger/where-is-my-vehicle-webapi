﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereIsMyVehicle.WebApi.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public VehicleType Type { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public GeoLocation Location { get; set; }

        public List<Sighting> Sightings { get; set; }
    }
}
