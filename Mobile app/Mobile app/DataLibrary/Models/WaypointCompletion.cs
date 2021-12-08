using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class WaypointCompletion
    {
        public long WaypointId { get; set; }
        public long RouteCompletionUserId { get; set; }
        public long RouteCompletionRouteId { get; set; }
        public int Points { get; set; }
    }
}
