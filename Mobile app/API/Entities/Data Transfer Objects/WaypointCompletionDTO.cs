using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Data_Transfer_Objects
{
    public class WaypointCompletionDTO
    {
        public long WaypointId { get; set; }
        public long RouteCompletionUserId { get; set; }
        public long RouteCompletionRouteId { get; set; }
    }
}
