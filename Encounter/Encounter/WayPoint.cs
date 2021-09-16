using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    public class WayPoint
    {
        public string name;
        public string coordinates;
        public string type;
        public string price;
        public string openingHours;
        public string closingTime;
        public string description;

        public WayPoint()
        {

        }

        public WayPoint(string name, string coordinates, string type, string price,
                        string openingHours, string closingTime, string description)
        {
            this.name = name;
            Coordinates = coordinates;
            Type = type;
            Price = price;
            OpeningHours = openingHours;
            ClosingTime = closingTime;
            Description = description;

            
        }

        public string GetName()
        {
            return name;
        }
        public void SetName(string name)
        {
            this.name = name;
        }

        public string Coordinates { get; set; }
        public string Type { get; set; }
        public string Price { get; set; }
        public string OpeningHours { get; set; }
        public string ClosingTime { get; set; }
        public string Description { get; set; }

        public string Output()
        {
            return ("Name:" + this.GetName() + "\n" +
                    "Coordinates:" + Coordinates + "\n" +
                    "Type:" + Type + "\n" + 
                    "Price:" + Price + "\n" +
                    "Opening hours:" + OpeningHours + "\n" +
                    "Closing hours:" + ClosingTime + "\n" +
                    "Description:" + Description + "\n");
        }

    }
}
