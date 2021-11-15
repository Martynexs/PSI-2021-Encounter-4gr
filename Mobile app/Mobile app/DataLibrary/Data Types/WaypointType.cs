using System;

namespace DataLibrary
{
    [Flags]
    public enum WaypointType
    {
        Other = 1,
        Shop = 2,
        Cafe = 4,
        Museum = 8,
        Church = 16,
        Sculpture = 32,
        Park = 64
    }
}