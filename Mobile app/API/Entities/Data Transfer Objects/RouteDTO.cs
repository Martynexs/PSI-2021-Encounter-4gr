namespace EncounterAPI.Data_Transfer_Objects
{
    public class RouteDTO
    {
        public long Id { get; set; }
        public long CreatorID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public double Rating { get; set; }
    }
}
