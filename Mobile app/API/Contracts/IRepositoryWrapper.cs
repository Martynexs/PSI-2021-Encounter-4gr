using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IRouteRepository Route { get; }
        IWaypointRepository Waypoint { get; }
        IRatingRepository Rating { get; }
        IUserRepository User { get; }
        Task SaveAsync();
    }
}
