using EncounterAPI.Models;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRouteCompletionRepository : IRepositoryBase<RouteCompletion>
    {
        void CreateRouteCompletion(RouteCompletion routeCompletion);
        void UpdateRouteCompletion(RouteCompletion routeCompletion);
        void DeleteRouteCompletion(RouteCompletion routeCompletion);
        Task<RouteCompletion> GetRouteCompletion(long routeId, long userId);
        Task<IEnumerable<RouteCompletion>> GetRouteCompletionsByRoute(long routeId);
        Task<IEnumerable<RouteCompletion>> GetRouteCompletionsByUser(long userId);
        Task<IEnumerable<RouteModel>> GetUserStartedRoutes(long userId);
    }
}
