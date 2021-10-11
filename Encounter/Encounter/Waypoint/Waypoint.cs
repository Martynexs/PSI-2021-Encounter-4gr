using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Encounter.Converters;

namespace Encounter
{
    public class WaypointClassMap : ClassMap<Waypoint>
    {
        public WaypointClassMap()
        {
            Map(w => w.Index).Name("Index");
            Map(w => w.Name).Name("Name");
            Map(w => w.Coord).Name("Coordinates").TypeConverter<CsvCoordinatesConverter>();
            Map(w => w.Type).Name("Type");
            Map(w => w.Price).Name("Price");
            Map(w => w.OpeningHours).Name("Opening");
            Map(w => w.ClosingTime).Name("Closing");
            Map(w => w.PhoneNumber).Name("Phone");
            Map(w => w.Description).Name("Description");
        }
    }

    public class Waypoint
    {
        [Name("Index")]
        public int Index { get; set; }
        [Name("Name")]
        public string Name { get; set; }
        [Name("Coordinates")]
        public Coordinates Coord { get; set; }
        [Name("Type")]
        public WaypointType Type { get; set; }
        [Name("Price")]
        public decimal Price { get; set; }
        [Name("Opening")]
        public DateTime OpeningHours { get; set; }
        [Name("Closing")]
        public DateTime ClosingTime { get; set; }
        [Name("Phone")]
        public String PhoneNumber { get; set; }
        [Name("Description")]
        public string Description { get; set; }

        public Waypoint()
        {

        }

    }
}
