﻿namespace Encounter.Models
{
    public class Route
    {
        public int ID { get; set; }
        public string CreatorID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public double Rating { get; set; }
        public int Raters { get; set; }
        public int RatingSum { get; set; }
        public string Description { get; set; }

        public Route()
        {
            
        }

        public Route(int id)
        {
            ID = id;
        }

        public override string ToString()
        {
            return ID + CreatorID + Name + Location;
        }
    }
}
