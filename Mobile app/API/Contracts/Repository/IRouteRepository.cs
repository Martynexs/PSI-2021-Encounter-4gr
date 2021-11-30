using EncounterAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRouteRepository : IRepositoryBase<RouteModel>
    {
        Task<IEnumerable<RouteModel>> GetAllRoutesAsync();
        Task<RouteModel> GetRouteByIdAsync(long Id);
        Task<IEnumerable<RouteModel>> GetUserRoutes(long Id);
        void CreateRoute(RouteModel route);
        void UpdateRoute(RouteModel route);
        void DeleteRoute(RouteModel route);
    }
}
