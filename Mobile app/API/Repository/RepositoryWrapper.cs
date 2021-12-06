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
                return _route;
            } 
        }

        private IWaypointRepository _waypoint;
        public IWaypointRepository Waypoint
        {
            get
            {
                return _waypoint;
            }
        }

        private IRatingRepository _rating;
        public IRatingRepository Rating
        {
            get
            {
                return _rating;
            }
        }

        private IUserRepository _user;
        public IUserRepository User
        {
            get
            {
                return _user;
            }
        }

        private IQuizRepository _quiz;
        public IQuizRepository Quiz
        {
            get
            {
                return _quiz;
            }
        }

        private IQuizAnswerRepository _quizAnswer;
        public IQuizAnswerRepository QuizAnswer
        {
            get
            {
                return _quizAnswer;
            }
        }

        private IRouteCompletionRepository _routeCompletion;
        public IRouteCompletionRepository RouteCompletion
        {
            get
            {
                return _routeCompletion;
            }
        }

        private IWaypointCompletionRepository _waypointCompletion;
        public IWaypointCompletionRepository WaypointCompletion
        {
            get
            {
                return _waypointCompletion;
            }
        }


        public RepositoryWrapper(EncounterContext repositoryContext,
                                 IRouteRepository routeRepository,
                                 IWaypointRepository waypointRepository,
                                 IUserRepository userRepository,
                                 IRatingRepository ratingRepository,
                                 IQuizRepository quizRepository,
                                 IQuizAnswerRepository quizAnswerRepository,
                                 IRouteCompletionRepository routeCompletionRepository,
                                 IWaypointCompletionRepository waypointCompletionRepository)
        {
            _repositoryContext = repositoryContext;
            _route = routeRepository;
            _waypoint = waypointRepository;
            _user = userRepository;
            _rating = ratingRepository;
            _quiz = quizRepository;
            _quizAnswer = quizAnswerRepository;
            _routeCompletion = routeCompletionRepository;
            _waypointCompletion = waypointCompletionRepository;
        }

        public async Task SaveAsync()
        {
            await _repositoryContext.SaveChangesAsync();
        }
    }
}
