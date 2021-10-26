using System;
using System.Collections.Generic;
using System.Linq;

namespace Encounter
{
    public static class WayPointTypeExtensions
    {
        public static string GetTypeString(this WaypointType t1)
        {
            return t1 switch
            {
                WaypointType.Shop => "Shop",
                WaypointType.Cafe => "Cafe",
                WaypointType.Museum => "Museum",
                WaypointType.Church => "Church",
                WaypointType.Sculpture => "Sculpture",
                WaypointType.Park => "Park",
                WaypointType.Other => "Other",
                _ => "Other",
            };
        }

        public static string GetColor(this WaypointType type)
        {
            return type switch
            {
                WaypointType.Shop => "#0392cf",      //Blue
                WaypointType.Cafe => "#f37736",      //Orange
                WaypointType.Museum => "#ee4035",    //Red
                WaypointType.Church => "#a64ca6",    //Purple 
                WaypointType.Sculpture => "#00e5e5", //Cyan 
                WaypointType.Park => "#7bc043",      //Green
                WaypointType.Other => "#fdf498",     //Yellow
                _ => "#fdf498",
            };
        }

        public static string GetTypeInitialDescription(this WaypointType t1)
        {
            return t1 switch
            {
                WaypointType.Shop => "Nice place to buy goods",
                WaypointType.Cafe => "Local drinks and snacks",
                WaypointType.Museum => "Arts and science house",
                WaypointType.Church => "House of prayers",
                WaypointType.Sculpture => "Masterpiece of local sculptor",
                WaypointType.Park => "Nature oasis",
                WaypointType.Other => "Other",
                _ => "Other",
            };
        }

        public static List<WaypointType> GetAllTypes()
        {
            return Enum.GetValues(typeof(WaypointType)).Cast<WaypointType>().ToList();
        }

        public static List<LabelValueItem<WaypointType>> GetLabelValueItems()
        {
            var result = new List<LabelValueItem<WaypointType>>();
            foreach (WaypointType t in Enum.GetValues(typeof(WaypointType)))
            {
                var label = GetTypeString(t);
                result.Add(new LabelValueItem<WaypointType>(label, t));
            }
            return result;
        }
    }
}
