using System.ComponentModel.DataAnnotations.Schema;

namespace EncounterAPI.Models
{
    public class Rating
    {
        public string Username { get; set; }
        [ForeignKey("Username")]
        public virtual User User { get; set; }
        public long RouteId { get; set; }
        [ForeignKey("RouteId")]
        public virtual RouteModel Route { get; set; }
        public int Value { get; set; }
    }
}
