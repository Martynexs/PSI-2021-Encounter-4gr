﻿using System;

namespace EncounterAPI.Data_Transfer_Objects
{
    public class WaypointDTO
    {
        public long Id { get; set; }
        public long RouteId { get; set; }
        public int Position { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime OpeningHours { get; set; }
        public DateTime ClosingTime { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Price { get; set; }
        public WaypointType Type { get; set; }
        public string PictureURL { get; set; }
        public int Points { get; set; }
    }
}
