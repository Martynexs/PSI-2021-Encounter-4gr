using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IWaypointCompletionRepository : IRepositoryBase<WaypointCompletion>
    {
        Task<IEnumerable<WaypointCompletion>> GetWaypointCompletions(long routeId, long userId);
        void CreateWaypointCompletion(WaypointCompletion waypointCompletion);
    }
}
