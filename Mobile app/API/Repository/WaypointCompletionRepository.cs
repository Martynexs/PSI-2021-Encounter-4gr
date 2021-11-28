using Contracts;
using EncounterAPI.Models;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class WaypointCompletionRepository : RepositoryBase<WaypointCompletion>, IWaypointCompletionRepository
    {
        public WaypointCompletionRepository(EncounterContext encounterContext)
            : base(encounterContext)
        {
        }

        public void CreateWaypointCompletion(WaypointCompletion waypointCompletion)
        {
            Create(waypointCompletion);
        }

        public async Task<IEnumerable<WaypointCompletion>> GetWaypointCompletions(long routeId, long userId)
        {
            return await FindByCondition(x => x.RouteCompletionRouteId == routeId && x.RouteCompletionUserId == userId).ToListAsync();
        }
    }
}
