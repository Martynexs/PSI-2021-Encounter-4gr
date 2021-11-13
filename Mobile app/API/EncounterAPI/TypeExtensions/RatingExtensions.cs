using EncounterAPI.Data_Transfer_Objects;
using EncounterAPI.Models;

namespace EncounterAPI.TypeExtensions
{
    public static class RatingExtensions
    {
        public static RatingDTO ToDTO(this Rating rating)
        {
            return new RatingDTO
            {
                UserId = rating.UserId,
                RouteId = rating.RouteId,
                Value = rating.Value
            };
        }

        public static Rating ToEFModel(this RatingDTO ratingDTO)
        {
            return new Rating
            {
                UserId = ratingDTO.UserId,
                RouteId = ratingDTO.RouteId,
                Value = ratingDTO.Value
            };
        }
    }
}
