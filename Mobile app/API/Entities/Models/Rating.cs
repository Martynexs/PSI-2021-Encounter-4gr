﻿using System.ComponentModel.DataAnnotations.Schema;

namespace EncounterAPI.Models
{
    public class Rating
    {
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserModel User { get; set; }
        public long RouteId { get; set; }
        [ForeignKey("RouteId")]
        public virtual RouteModel Route { get; set; }
        public int Value { get; set; }
    }
}
