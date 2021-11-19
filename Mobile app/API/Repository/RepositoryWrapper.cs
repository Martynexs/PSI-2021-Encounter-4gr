using Contracts;
using EncounterAPI.Models;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private EncounterContext _repositoryContext;
        private IRouteRepository _route;
        public IRouteRepository Route 
        {
            get
            {
                if(_route == null)
                {
                    _route = new RouteRepository(_repositoryContext);
                }
                return _route;
            } 
        }

        public RepositoryWrapper(EncounterContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task SaveAsync()
        {
            await _repositoryContext.SaveChangesAsync();
        }
    }
}
