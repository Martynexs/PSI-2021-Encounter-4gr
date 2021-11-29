using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IRouteRepository Route { get; }
        IWaypointRepository Waypoint { get; }
        IRatingRepository Rating { get; }
        IUserRepository User { get; }
        IRouteCompletionRepository RouteCompletion { get; }
        IWaypointCompletionRepository WaypointCompletion { get; }
        IQuizRepository Quiz { get; }
        IQuizAnswerRepository QuizAnswer { get; }
        Task SaveAsync();
    }
}
