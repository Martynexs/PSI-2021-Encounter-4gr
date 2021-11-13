using System.Collections.Generic;
using Newtonsoft.Json;



namespace Map3.Views
{
    public class LatLong
    {
        public double Lat { get; set; }
        public double Long { get; set; }
    }

    public  class DirectionResponse
    {
        [JsonProperty("routes")]
        public List<Route> Routes { get; set; }

        [JsonProperty("waypoints")]
        public List<Waypoint> Waypoints { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

    }
    public  class Route
    {
        [JsonProperty("legs")]
        public List<Leg> Legs { get; set; }
      
        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }

    }
    public  class Leg
    {
  
        [JsonProperty("steps")]
        public List<Step> Steps { get; set; }
    }
    public class Step
    {
        [JsonProperty("intersections")]
        public List<Intersection> Intersections { get; set; }

        [JsonProperty("maneuver")]
        public Maneuver Maneuver { get; set; }

    }
    public class Waypoint
    {
        //[JsonProperty("hint")]
        //public string Hint { get; set; }

        //[JsonProperty("distance")]
        //public double Distance { get; set; }

        //[JsonProperty("name")]
        //public string Name { get; set; }

        //[JsonProperty("location")]
        //public List<double> Location{ get; set; }

    }
    public class Maneuver
    {
        [JsonProperty("location")]
        public List<double> Location { get; set; }
    }
    public class Intersection
    {
        [JsonProperty("location")]
        public List<double> Location { get; set; }
    }
}

