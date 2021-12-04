using EncounterAPI.Data_Transfer_Objects;
using EncounterAPI.Models;

namespace EncounterAPI.TypeExtensions
{
    public static class WaypointExtension
    {
        public static WaypointDTO ToDTO(this Waypoint wp)
        {
            return new WaypointDTO
            {
                Id = wp.Id,
                RouteId = wp.RouteId,
                Position = wp.Position,
                Name = wp.Name,
                Longitude = wp.Longitude,
                Latitude = wp.Latitude,
                Description = wp.Description,
                OpeningHours = wp.OpeningHours,
                ClosingTime = wp.ClosingTime,
                Price = wp.Price,
                Type = wp.Type,
                PhoneNumber = wp.PhoneNumber,
                PictureURL = wp.PictureURL,
                Points = wp.Points
            };
        }

        public static Waypoint ToEFModel(this WaypointDTO waypointDTO)
        {
            return new Waypoint
            {
                Id = waypointDTO.Id,
                RouteId = waypointDTO.RouteId,
                Position = waypointDTO.Position,
                Name = waypointDTO.Name,
                Longitude = waypointDTO.Longitude,
                Latitude = waypointDTO.Latitude,
                Description = waypointDTO.Description,
                OpeningHours = waypointDTO.OpeningHours,
                ClosingTime = waypointDTO.ClosingTime,
                Price = waypointDTO.Price,
                Type = waypointDTO.Type,
                PhoneNumber = waypointDTO.PhoneNumber,
                PictureURL = waypointDTO.PictureURL,
                Points = waypointDTO.Points
            };
        }


    }
}
