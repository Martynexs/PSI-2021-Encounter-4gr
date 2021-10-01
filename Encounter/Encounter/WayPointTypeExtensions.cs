using System;
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
                case WaypointType.SHOP:
                    return "Shop";
                case WaypointType.CAFE:
                    return "Cafe";
                case WaypointType.MUSEUM:
                    return "Museum";
                case WaypointType.CHURCH:
                    return "Church";
                case WaypointType.SCULPTURE:
                    return "Sculpture";
                case WaypointType.PARK:
                    return "Park";
                default:
                    return "Others";

            }
        }
    }
}
