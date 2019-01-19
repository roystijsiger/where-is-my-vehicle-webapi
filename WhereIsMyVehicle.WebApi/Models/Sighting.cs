namespace WhereIsMyVehicle.WebApi.Models
{
    public class Sighting
    {
        public int Id { get; set; }
        public Vehicle Vehicle { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string Photo { get; set; }
    }
}