namespace DataLibrary.Models
{
    public class Rating
    {
        public long UserId { get; set; }
        public long RouteId { get; set; }
        public int Value { get; set; }
    }
}
