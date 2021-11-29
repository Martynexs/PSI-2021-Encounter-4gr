using EncounterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class RouteCompletion
    {
        public long RouteId { get; set; }
        public long UserId { get; set; }
        public virtual RouteModel Route { get; set; }
        public virtual UserModel User { get; set; }
        public int Points { get; set; }
        public virtual List<WaypointCompletion> CompletedWaypoints { get; set; }
        public DateTime LastVisit { get; set; }
    }
}
