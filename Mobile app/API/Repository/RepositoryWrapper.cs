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

        private IWaypointRepository _waypoint;
        public IWaypointRepository Waypoint
        {
            get
            {
                if(_waypoint == null)
                {
                    _waypoint = new WaypointRepository(_repositoryContext);
                }
                return _waypoint;
            }
        }

        private IRatingRepository _rating;
        public IRatingRepository Rating
        {
            get
            {
                if (_rating == null)
                {
                    _rating = new RatingRepository(_repositoryContext);
                }
                return _rating;
            }
        }

        private IUserRepository _user;
        public IUserRepository User
        {
            get
            {
                if(_user == null)
                {
                    _user = new UserRopository(_repositoryContext);
                }
                return _user;
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
