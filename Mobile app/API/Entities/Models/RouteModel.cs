﻿using System.Collections.Generic;

namespace EncounterAPI.Models
{
    public class RouteModel
    {
        public long Id { get; set; }
        public virtual UserModel Creator { get; set; }
        public long CreatorID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Raters { get; set; }
        public double Rating { get; set; }
        public int RateSum { get; set; }
        public List<Waypoint> Waypoints { get; set; }
    }
}
