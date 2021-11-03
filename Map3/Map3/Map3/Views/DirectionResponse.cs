using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Map3.ViewModels;
using Map3.Models;
using System.Collections.Generic;



namespace Map3.Views
{
        public class LatLong
        {
            public double Lat { get; set; }
            public double Long { get; set; }
        }

        public partial class DirectionResponse
        {
          [JsonProperty("routes")]
          public List <Route> Routes { get; set; }

          [JsonProperty("waypoints")]
          public List<Waypoint> Waypoints { get; set; }

          [JsonProperty("code")]
          public string Code { get; set; }

        }
        public partial class Route
        {
        //[JsonProperty("geometry")]
        //public string Geometry{ get; set; }

        [JsonProperty("legs")]
        public List<Leg>Legs { get; set; }

        //[JsonProperty("weight_name")]
        //public string WeightName { get; set; }

        //[JsonProperty("weight")]
        //public double Weight { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }

        }
    public partial class Leg
    {
        //[JsonProperty("summary")]
        //public string Summary { get; set; }

        //[JsonProperty("weight")]
        //public double Weight { get; set; }

        //[JsonProperty("duration")]
        //public string Duration { get; set; }

        [JsonProperty("steps")]
        public List <Step> Steps { get; set; }

        //[JsonProperty("distance")]
        //public double Distance { get; set; }


    }
    public partial class Step
    {
        [JsonProperty("intersections")]
        public List <Intersection> Intersections{ get; set; }

        //[JsonProperty("driving_side")]
        //public string DrivingSide { get; set; }

        //[JsonProperty("geometry")]
        //public string Geometry { get; set; }

        //[JsonProperty("mode")]
        //public double Mode { get; set; }

        [JsonProperty("maneuver")]
        public Maneuver Maneuver { get; set; }

        //[JsonProperty("weight")]
        //public double Weight { get; set; }

    }
    public partial class Waypoint
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
    public partial class Maneuver
    {
        //[JsonProperty("hint")]
        //public string Hint { get; set; }

        //[JsonProperty("distance")]
        //public double Distance { get; set; }

        //[JsonProperty("name")]
        //public string Name { get; set; }

        [JsonProperty("location")]
        public List<double> Location { get; set; }

    }
    public partial class Intersection
    {
        //[JsonProperty("out")]
        //public int Out { get; set; }

        //[JsonProperty("in")]
        //public int In { get; set; }

        //[JsonProperty("entry")]
        //public List<bool> Entry { get; set; }

        //[JsonProperty("bearings")]
        //public List <int> Bearings { get; set; }

        [JsonProperty("location")]
        public List<double> Location { get; set; }

    }


}

