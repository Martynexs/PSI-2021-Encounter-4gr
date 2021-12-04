using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class RouteCompletion
    {
        public long RouteId { get; set; }
        public long UserId { get; set; }
        public int Points { get; set; }
        public DateTime LastVisit { get; set; }
    }
}
