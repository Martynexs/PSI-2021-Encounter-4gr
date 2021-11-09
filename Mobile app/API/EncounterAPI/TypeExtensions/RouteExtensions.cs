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

        public static RouteModel ToEFModel(this RouteDTO routeDTO)
        {
            return new RouteModel
            {
                Id = routeDTO.Id,
                CreatorUsername = routeDTO.CreatorUsername,
                Name = routeDTO.Name,
                Location = routeDTO.Location,
                Description = routeDTO.Description,
                Rating = routeDTO.Rating
            };
        }
    }
}
