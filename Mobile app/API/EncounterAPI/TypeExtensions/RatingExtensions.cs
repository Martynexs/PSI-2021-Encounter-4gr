using EncounterAPI.Data_Transfer_Objects;
using EncounterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncounterAPI.TypeExtensions
{
    public static class RatingExtensions
    {
        public static RatingDTO ToDTO(this Rating rating)
        {
            return new RatingDTO
            {
                Id = rating.Id,
                UserId = rating.UserId,
                RouteId = rating.RouteId,
                Value = rating.Value
            };
        }
    }
}
