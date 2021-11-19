using System;

namespace EncounterAPI.Models
{
    public class Waypoint
    {
        public long Id { get; set; }
        public long RouteId { get; set; }
        public RouteModel Route { get; set; }
        public int Position { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime OpeningHours { get; set; }
        public DateTime ClosingTime { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Price { get; set; }
        public WaypointType Type { get; set; }
    }
}
