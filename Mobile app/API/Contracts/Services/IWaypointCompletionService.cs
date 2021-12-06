using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IWaypointCompletionService
    {
        Task CreateWaypointCompletion(WaypointCompletion waypointCompletion);
        Task<IEnumerable<WaypointCompletion>> GetWaypointCompletions(long routeId, long userId);
        bool WaypointCompletionExists(long routeId, long userId);
    }
}
