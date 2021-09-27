using System;
using System.Collections.Generic;
using System.Text;

namespace Encounter
{
    public class Waypoint
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Coordinates { get; set; }
        public string Type { get; set; }
        public string Price { get; set; }
        public string OpeningHours { get; set; }
        public string ClosingTime { get; set; }
        public string Description { get; set; }
        public int Index { get; set; }

        public Waypoint()
        {

        }

        public Waypoint(int number, string name, string coordinates, string type, string price,
                        string openingHours, string closingTime, string description, int index)
        {
            Number = number;
            Name = name;
            Coordinates = coordinates;
            Type = type;
            Price = price;
            OpeningHours = openingHours;
            ClosingTime = closingTime;
            Description = description;
            Index = index;
        }

        public string Output()
        {
            return  "Name:" + Name + "\n" +
                    "Coordinates:" + Coordinates + "\n" +
                    "Type:" + Type + "\n" + 
                    "Price:" + Price + "\n" +
                    "Opening hours:" + OpeningHours + "\n" +
                    "Closing hours:" + ClosingTime + "\n" +
                    "Description:" + Description + "\n";
        }

        public void Update(int number, string name, string coordinates, string type, string price, string opening, string closing, string descriptions, int index)
        {
            this.Number = number;
            this.Name = name;
            this.Coordinates = coordinates;
            this.Type = type;
            this.Price = price;
            this.OpeningHours = opening;
            this.ClosingTime = closing;
            this.Description = descriptions;
        }

    }
}
