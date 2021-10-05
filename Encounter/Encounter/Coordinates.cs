using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter
{
    public struct Coordinates
    {
        public double latitude, longitude;

        public Coordinates(double p1, double p2)
        {
            latitude = p1;
            longitude = p2;
        }

        public override string ToString()
        {
            return longitude + "," + latitude;
        }
    };
}
