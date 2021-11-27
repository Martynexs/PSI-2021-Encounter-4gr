using EncounterAPI.Models;
using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class WaypointRepository : RepositoryBase<Waypoint>, IWaypointRepository
    {

        public WaypointRepository(EncounterContext encounterContext)
            : base(encounterContext)
        {
        }

        public void CreateWaypoint(Waypoint waypoint)
        {
            Create(waypoint);
        }

        public void DeleteWaypoint(Waypoint waypoint)
        {
            Delete(waypoint);
        }

        public async Task<IEnumerable<Waypoint>> GetAllWaypoints()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<Waypoint> GetWaypointById(long Id)
        {
            return await FindByCondition(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Waypoint>> GetWaypointsByRoute(long routeId)
        {
            return await FindByCondition(x => x.RouteId == routeId).ToListAsync();
        }

        public void UpdateWaypoint(Waypoint waypoint)
        {
            Update(waypoint);
        }
    }
}
