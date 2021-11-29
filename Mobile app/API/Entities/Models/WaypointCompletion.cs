using EncounterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class WaypointCompletion
    {
        public long WaypointId { get; set; }
        public long RouteCompletionUserId { get; set; }
        public long RouteCompletionRouteId { get; set; }
        public virtual Waypoint Waypoint {get; set;}
        public virtual RouteCompletion RouteCompletion { get; set; }
    }
}
