using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EncounterAPI.Models
{
    public class UserModel
    {
        public long ID { get; set; }
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public virtual List<Rating> Ratings { get; set; }
        public virtual List<RouteModel> Routes { get; set; }
    }
}
