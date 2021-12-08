using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IScoresService
    {
        Task<IEnumerable<dynamic>> GetRouteLeaderboard(long routeId);
    }
}
