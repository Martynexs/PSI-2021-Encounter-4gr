namespace EncounterAPI.Data_Transfer_Objects
{
    public class RatingDTO
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public long RouteId { get; set; }
        public int Value { get; set; }
    }
}
