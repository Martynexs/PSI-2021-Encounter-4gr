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
                Username = rating.Username,
                RouteId = rating.RouteId,
                Value = rating.Value
            };
        }

        public static Rating ToEFModel(this RatingDTO ratingDTO)
        {
            return new Rating
            {
                Username = ratingDTO.Username,
                RouteId = ratingDTO.RouteId,
                Value = ratingDTO.Value
            };
        }
    }
}
