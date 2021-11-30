using DataLibrary.Exceptions;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary
{
    public class EncounterProcessor
    {
        public const string _apiAdress = "https://encounterapi.conveyor.cloud/swagger/index.html";

        private static readonly Lazy<EncounterProcessor> _encounterProcessor =
            new Lazy<EncounterProcessor>(() => new EncounterProcessor());
        public static EncounterProcessor Instanse { get => _encounterProcessor.Value; }
        private EncounterProcessor()
        {
        }

        private readonly ApiHelper _apiHelper = new ApiHelper();

        public event Action UnauthorisedHttpRequestEvent;

        public void EnableJWTAuthetication(string jwt)
        {
            _apiHelper.SetJWT(jwt);
        }

        public async Task<Route> GetRoute(long id)
        {
            var url = $"{ _apiAdress }/api/route/{ id }";

            var route = await _apiHelper.HttpGet<Route>(url);
            return route;
        }

        public async Task<List<Route>> GetAllRoutes()
        {
            Console.WriteLine("Pateko i API");
            var url = $"{ _apiAdress }/api/route";
            try
            {
            var routes = await _apiHelper.HttpGet<List<Route>>(url);
            return routes;
            }
            catch (Exception ex)
            {
                var exc = ex.Message;
                throw new Exception();
            }
        }

        public async Task<List<Route>> GetUserRoutes(long id)
        {
            var url = $"{ _apiAdress }/api/route/user/{ id }";
            var routes = await _apiHelper.HttpGet<List<Route>>(url);
            return routes;
        }

        public async Task<List<Waypoint>> GetWaypoints(long routeId)
        {
            var url = $"{ _apiAdress }/api/route/{ routeId }/Waypoints";

            var waypoints = await _apiHelper.HttpGet<List<Waypoint>>(url);
            return waypoints;
        }

        public async Task<Waypoint> GetWaypoint(long waypointId)
        {
            var url = $"{ _apiAdress }/api/waypoints/{ waypointId }";

            var waypoint = await _apiHelper.HttpGet<Waypoint>(url);
            return waypoint;
        }

        public async Task<Route> CreateRoute(Route route)
        {
            var url = $"{ _apiAdress }/api/route";

            var createdRoute = await _apiHelper.HttpPost<Route>(url, route);
            return createdRoute;
        }

        public async Task UpdateRoute(long id, Route route)
        {
            var url = $"{ _apiAdress }/api/route/{ id }";

            await _apiHelper.HttpPut<Route>(url, route);
        }

        public async Task DeleteRoute(long id)
        {
            try
            {
                var url = $"{ _apiAdress }/api/route/{ id }";
                await _apiHelper.HttpDelete(url);
            }
            catch(UnauthorizedHttpRequestException)
            {
                UnauthorisedHttpRequestEvent.Invoke();
            }
        }

        public async Task<Waypoint> CreateWaypoint(Waypoint waypoint)
        {
            var url = $"{ _apiAdress }/api/waypoints";

            var createdWaypoint = await _apiHelper.HttpPost<Waypoint>(url, waypoint);
            return createdWaypoint;
        }

        public async Task UpdateWaypoint(long id, Waypoint waypoint)
        {
            var url = $"{ _apiAdress }/api/waypoints/{ id }";

            await _apiHelper.HttpPut<Waypoint>(url, waypoint);
        }

        public async Task UpdateUser(long userId, User user)
        {
            var url = $"{ _apiAdress }/api/Users/{ userId }";

            await _apiHelper.HttpPut<User>(url, user);
        }

        public async Task DeleteWaypoint(long id)
        {
            var url = $"{ _apiAdress }/api/waypoints/{ id }";

            await _apiHelper.HttpDelete(url);
        }

        public async Task SubmitRating(Rating rating)
        {
            var url = $"{ _apiAdress }/api/ratings/{ rating.RouteId }/{ rating.UserId }";

            await _apiHelper.HttpPut<Rating>(url, rating);
        }

        public async Task<Rating> GetRating(long routeId, long userId)
        {
            var url = $"{ _apiAdress }/api/ratings/{ routeId }/{ userId }";

            var rating = await _apiHelper.HttpGet<Rating>(url);
            return rating;
        }

        public async Task<User> GetUser(string username)
        {
            try
            {
                var url = $"{ _apiAdress }/api/Users/{ username }";
                var user = await _apiHelper.HttpGet<User>(url);

                return user;
            }
            catch(UnauthorizedHttpRequestException)
            {
                UnauthorisedHttpRequestEvent.Invoke();
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<User> RegisterUser(User user)
        {
            var url = $"{ _apiAdress }/api/Users";

            var createdUser = await _apiHelper.HttpPost<User>(url, user);
            return createdUser;
        }

        public async Task<string> GetAuthenticationToken(string username, string password)
        {
            var url = $"{ _apiAdress }/token?username={ username }&password={password}";

            var logininfo = await _apiHelper.HttpPost<LoginInfo>(url, new LoginInfo { Username = username, Password = password });
            var token = logininfo.access_token;
            return token;
        }

        public async Task<Quiz> GetWaypointQuiz(long waypointID)
        {
            var url = $"{ _apiAdress }/api/Waypoints/{ waypointID }/Quiz";

            var quiz = await _apiHelper.HttpGet<Quiz>(url);
            return quiz;
        }

        public async Task<Quiz> CreateQuiz(Quiz quiz)
        {
            var url = $"{ _apiAdress }/api/Quizzes";

            var createdQuiz = await _apiHelper.HttpPost<Quiz>(url, quiz);
            return createdQuiz;
        }

        public async Task UpdateQuiz(Quiz quiz)
        {
            var url = $"{ _apiAdress }/api/Quizzes/{ quiz.Id }";

            await _apiHelper.HttpPut<Quiz>(url, quiz);
        }

        public async Task DeleteQuiz(long quizId)
        {
            var url = $"{ _apiHelper }/api/{quizId}";

            await _apiHelper.HttpDelete(url);
        }

        public async Task<QuizAnswer> CreateQuizAnswer(QuizAnswer answer)
        {
            var url = $"{ _apiAdress }/api/QuizAnswers";

            var createdAnswer = await _apiHelper.HttpPost<QuizAnswer>(url, answer);
            return createdAnswer;
        }

        public async Task UpdateQuizAnswer(QuizAnswer answer)
        {
            var url = $"{ _apiAdress }/api/QuizAnswers/{ answer.Id }";

            await _apiHelper.HttpPut<QuizAnswer>(url, answer);
        }

        public async Task DeleteQuizAnswer(long quizAnswerID)
        {
            var url = $"{ _apiAdress }/api/QuizAnswers/{ quizAnswerID }";

            await _apiHelper.HttpDelete(url);
        }

        public async Task<QuizAnswer> GetQuizAnswer(long quizAnswerID)
        {
            var url = $"{ _apiAdress }/api/QuizAnswers/{ quizAnswerID }";

            var answer = await _apiHelper.HttpGet<QuizAnswer>(url);
            return answer;
        }





    }
}
