using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EncounterAPI.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public virtual List<Rating> Ratings { get; set; }
        public virtual List<RouteModel> Routes { get; set; }
    }
}
