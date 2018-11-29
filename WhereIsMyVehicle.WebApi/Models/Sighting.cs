namespace WhereIsMyVehicle.WebApi.Models
{
    public class Sighting
    {
        public int Id { get; set; }
        public Vehicle Vehicle { get; set; }
        public GeoLocation Location { get; set; }
        public string Photo { get; set; }
    }
}