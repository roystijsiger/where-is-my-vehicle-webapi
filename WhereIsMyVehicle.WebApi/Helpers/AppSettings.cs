using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereIsMyVehicle.WebApi.Helpers
{
    public class AppSettings
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Secret { get; set; }
        public string DatabaseName { get; set; }
    }
}
