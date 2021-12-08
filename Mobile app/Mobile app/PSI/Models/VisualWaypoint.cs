namespace PSI.Models
{
    public class VisualWaypoint
    {
        public long Id { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string PickString { get; set; }

        public override bool Equals(object obj)
        {
            return obj is VisualWaypoint waypoint &&
                   Id == waypoint.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
