using EncounterAPI.Data_Transfer_Objects;
using EncounterAPI.Models;

namespace EncounterAPI.TypeExtensions
{
    public static class RouteExtensions
    {
        public static RouteDTO ToDTO(this RouteModel route)
        {
            return new RouteDTO
            {
                Id = route.Id,
                CreatorUsername = route.CreatorUsername,
                Name = route.Name,
                Location = route.Location,
                Description = route.Description,
                Rating = route.Rating
            };
        }
    }
}
