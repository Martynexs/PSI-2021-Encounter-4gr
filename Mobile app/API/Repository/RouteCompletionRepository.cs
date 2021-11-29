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
    public class RouteCompletionRepository : RepositoryBase<RouteCompletion>, IRouteCompletionRepository
    {
        public RouteCompletionRepository(EncounterContext encounterContext)
            : base(encounterContext)
        {
        }

        public void CreateRouteCompletion(RouteCompletion routeCompletion)
        {
            Create(routeCompletion);
        }

        public void DeleteRouteCompletion(RouteCompletion routeCompletion)
        {
            Delete(routeCompletion);
        }

        public async Task<RouteCompletion> GetRouteCompletion(long routeId, long userId)
        {
            return await FindByCondition(x => x.RouteId == routeId && x.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RouteCompletion>> GetRouteCompletionsByRoute(long routeId)
        {
            return await FindByCondition(x => x.RouteId == routeId).ToListAsync();
        }

        public async Task<IEnumerable<RouteCompletion>> GetRouteCompletionsByUser(long userId)
        {
            return await FindByCondition(x => x.UserId == userId).ToListAsync();
        }

        public void UpdateRouteCompletion(RouteCompletion routeCompletion)
        {
            Update(routeCompletion);
        }
    }
}
