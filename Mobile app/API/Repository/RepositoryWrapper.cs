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

        private IQuizRepository _quiz;
        public IQuizRepository Quiz
        {
            get
            {
                if (_quiz == null)
                {
                    _quiz = new QuizRepository(_repositoryContext);
                }
                return _quiz;
            }
        }

        private IQuizAnswerRepository _quizAnswer;
        public IQuizAnswerRepository QuizAnswer
        {
            get
            {
                if (_quizAnswer == null)
                {
                    _quizAnswer = new QuizAnswerRepository(_repositoryContext);
                }
                return _quizAnswer;
            }
        }

        private IRouteCompletionRepository _routeCompletion;
        public IRouteCompletionRepository RouteCompletion
        {
            get
            {
                if(_routeCompletion == null)
                {
                    _routeCompletion = new RouteCompletionRepository(_repositoryContext);
                }
                return _routeCompletion;
            }
        }

        private IWaypointCompletionRepository _waypointCompletion;
        public IWaypointCompletionRepository WaypointCompletion
        {
            get
            {
                if(_waypointCompletion == null)
                {
                    _waypointCompletion = new WaypointCompletionRepository(_repositoryContext);
                }
                return _waypointCompletion;
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
