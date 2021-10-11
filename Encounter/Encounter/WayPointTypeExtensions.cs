using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter
{

    public static class WayPointTypeExtensions
    {

       public static string GetTypeString(this WaypointType t1)
        {
            switch(t1)
            {
                case WaypointType.Shop:
                    return "Shop";
                case WaypointType.Cafe:
                    return "Cafe";
                case WaypointType.Museum:
                    return "Museum";
                case WaypointType.Church:
                    return "Church";
                case WaypointType.Sculpture:
                    return "Sculpture";
                case WaypointType.Park:
                    return "Park";
                default:
                    return "Other";
            }
        }

        public static string GetTypeInitialDescription(this WaypointType t1)
        {
            switch (t1)
            {
                case WaypointType.Shop:
                    return "Nice place to buy goods";
                case WaypointType.Cafe:
                    return "Local drinks and snacks";
                case WaypointType.Museum:
                    return "Arts and science house";
                case WaypointType.Church:
                    return "House of prayers";
                case WaypointType.Sculpture:
                    return "Masterpiece of local sculptor";
                case WaypointType.Park:
                    return "Nature oasis";
                default:
                    return "Other";
            }
        }

        public static List<LabelValueItem<WaypointType>> GetAllTypes()
        {
           List<LabelValueItem<WaypointType>> result = new List<LabelValueItem<WaypointType>>();
           foreach (WaypointType t in Enum.GetValues(typeof(WaypointType)))
            {
                string label = GetTypeString(t);
                result.Add(new LabelValueItem<WaypointType>(label, t));
            }
           return result;
        } 
    }
}
