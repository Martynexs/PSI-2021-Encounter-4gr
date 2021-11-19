using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IRouteRepository Route { get; }
        Task SaveAsync();
    }
}
