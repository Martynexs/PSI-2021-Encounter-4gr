﻿namespace DataLibrary.Models
{
    public class Route
    {
        public long Id { get; set; }
        public long CreatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public double Rating { get; set; }
        public double Distances { get; set; }
    }
}
