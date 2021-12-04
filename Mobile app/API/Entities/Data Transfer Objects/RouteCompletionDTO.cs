using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Data_Transfer_Objects
{
    public class RouteCompletionDTO
    {
        public long RouteId { get; set; }
        public long UserId { get; set; }
        public int Points { get; set; }
        public DateTime LastVisit { get; set; }
    }
}
