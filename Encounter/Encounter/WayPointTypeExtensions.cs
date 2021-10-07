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

        public static string GetTypeInitialDescription(this WaypointType t1)
        {
            switch (t1)
            {
                case WaypointType.SHOP:
                    return "Nice place to buy goods";
                case WaypointType.CAFE:
                    return "Local drinks and snacks";
                case WaypointType.MUSEUM:
                    return "Arts and science house";
                case WaypointType.CHURCH:
                    return "House of prayers";
                case WaypointType.SCULPTURE:
                    return "Masterpiece of local sculptor";
                case WaypointType.PARK:
                    return "Nature oasis";
                default:
                    return "Others";

            }
        }

        public static List<LabelValueItem<WaypointType>> GetAllTypes()
        {
         
           List<LabelValueItem<WaypointType>> result = new List<LabelValueItem<WaypointType>>();
           foreach (WaypointType t in Enum.GetValues(typeof(WaypointType)))
            {
                String label = WayPointTypeExtensions.GetTypeString(t);
                result.Add(new LabelValueItem<WaypointType>(label, t));
            }
           return result;
        } 
    }
}
