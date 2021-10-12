namespace Encounter
{
    public struct Coordinates
    {
        public double Latitude { get; set; } 
        public double Longitude { get; set; }

        public Coordinates(double p1, double p2)
        {
            Latitude = p1;
            Longitude = p2;
        }

        public override string ToString()
        {
            return Longitude + "," + Latitude;
        }
    };
}
