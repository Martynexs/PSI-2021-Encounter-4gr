namespace EncounterAPI.Models
{
    public class Rating
    {
        public long Id { get; set; }
        public virtual User User { get; set; }
        public long UserId { get; set; }
        public RouteModel Route { get; set; }
        public long RouteId { get; set; }
        public int Value { get; set; }
    }
}
