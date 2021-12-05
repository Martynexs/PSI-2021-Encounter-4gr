using Contracts.Services;
using EncounterAPI.Models;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class WaypointCompletionService : IWaypointCompletionService
    {
        private EncounterContext _repository;
        public WaypointCompletionService(EncounterContext context)
        {
            _repository = context;
        }

        public async Task CreateWaypointCompletion(WaypointCompletion waypointCompletion)
        {
            _repository.WaypointCompletions.Add(waypointCompletion);
            await _repository.SaveChangesAsync();
            await UpdateRouteCompletionPoints(waypointCompletion);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<WaypointCompletion>> GetWaypointCompletions(long routeId, long userId)
        {
            return await _repository.WaypointCompletions.Where(x => x.RouteCompletionRouteId == routeId && x.RouteCompletionUserId == userId).ToListAsync();
        }

        public bool WaypointCompletionExists(long routeId, long userId)
        {
            return _repository.WaypointCompletions.Where(x => x.RouteCompletionRouteId == routeId && x.RouteCompletionUserId == userId).Any();
        }

        private async Task UpdateRouteCompletionPoints(WaypointCompletion waypointCompletion)
        {
            var routeCompletion = await _repository.RouteCompletions.FindAsync(waypointCompletion.RouteCompletionRouteId, waypointCompletion.RouteCompletionUserId);

            var pt = _repository.WaypointCompletions.Join(_repository.Waypoints,
                                                                cpl => cpl.WaypointId,
                                                                wp => wp.Id,
                                                                (cpl, wp) => new
                                                                {
                                                                    WaypointId = cpl.WaypointId,
                                                                    RouteId = cpl.RouteCompletionRouteId,
                                                                    UserId = cpl.RouteCompletionUserId,
                                                                    Points = wp.Points
                                                                });
            var points = (from pts in pt
                          where pts.RouteId == routeCompletion.RouteId && pts.UserId == routeCompletion.UserId
                          group pts by pts.RouteId into grp
                          select new
                          {
                              Sum = grp.Sum(x => x.Points)
                          }).First().Sum;

            routeCompletion.Points = points;
        }
    }
}
