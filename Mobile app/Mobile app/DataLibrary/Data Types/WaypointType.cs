using System;

namespace DataLibrary
{
    [Flags]
    public enum WaypointType
    {
        Other = 0,
        Shop = 1,
        Cafe = 2,
        Museum = 3,
        Church = 4,
        Sculpture = 5,
        Park = 6
    }
}