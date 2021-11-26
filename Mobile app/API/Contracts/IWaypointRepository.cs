using EncounterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IWaypointRepository : IRepositoryBase<Waypoint>
    {
        Task<IEnumerable<Waypoint>> GetAllWaypoints();
        Task<Waypoint> GetWaypointById(long Id);
        Task<IEnumerable<Waypoint>> GetWaypointsByRoute(long routeId);
        void CreateWaypoint(Waypoint waypoint);
        void DeleteWaypoint(Waypoint waypoint);
        void UpdateWaypoint(Waypoint waypoint);
    }
}
