using System;
using System.Collections.Generic;
using System.Text;

namespace Encounter
{

    public class Waypoint
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public Coordinates Coord { get; set; }
        public WaypointType Type { get; set; }
        public decimal Price { get; set; }
        public DateTime OpeningHours { get; set; }
        public DateTime ClosingTime { get; set; }
        public string Description { get; set; }

        public Waypoint()
        {

        }

    }
}
