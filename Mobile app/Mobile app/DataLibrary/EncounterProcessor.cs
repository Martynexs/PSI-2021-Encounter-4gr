using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataLibrary
{
    public static class EncounterProcessor
    {
        private static readonly string _apiAdress = "localhost:44309";

        public static async Task<Route> GetRoute(long id)
        {
            var url = $"https://{ _apiAdress }/api/route/{ id }";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if(response.IsSuccessStatusCode)
                {
                    Route route = await response.Content.ReadAsAsync<Route>();
                    return route;
                }
                else
                { 
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<Route>> GetAllRoutes()
        {
            var url = $"https://{ _apiAdress }/api/route";
            try
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var routes = await response.Content.ReadAsAsync<List<Route>>();

                        return routes;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                var exc = ex.Message;
                throw new Exception();
            }
        }

        public static async Task<List<Waypoint>> GetWaypoints(long routeId)
        {
            var url = $"https://{ _apiAdress }/api/route/{ routeId }/Waypoints";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var waypoints = await response.Content.ReadAsAsync<List<Waypoint>>();

                    return waypoints;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<Route> CreateRoute(Route route)
        {
            var url = $"https://{ _apiAdress }/api/route";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, route))
            {
                if (response.IsSuccessStatusCode)
                {
                    var createdRoute = await response.Content.ReadAsAsync<Route>();

                    return createdRoute;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task UpdateRoute(long id, Route route)
        {
            var url = $"https://{ _apiAdress }/api/route/{ id }";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync(url, route))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task DeleteRoute(long id)
        {
            var url = $"https://{ _apiAdress }/api/route/{ id }";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync(url))
            {
                if (!response.IsSuccessStatusCode)
                { 
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<Waypoint> CreateWaypoint(Waypoint waypoint)
        {
            var url = $"https://{ _apiAdress }/api/waypoints";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, waypoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var createdWaypoint = await response.Content.ReadAsAsync<Waypoint>();

                    return createdWaypoint;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task UpdateWaypoint(long id, Waypoint waypoint)
        {
            var url = $"https://{ _apiAdress }/api/waypoints/{ id }";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync(url, waypoint))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task DeleteWaypoint(long id)
        {
            var url = $"https://{ _apiAdress }/api/waypoints/{ id }";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync(url))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task SubmitRating(Rating rating)
        {
            var url = $"https://{ _apiAdress }/api/ratings/{ rating.RouteId }/{ rating.Username }";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync(url, rating))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<Rating> GetRating(long routeId, string username)
        {
            var url = $"https://{ _apiAdress }/api/ratings/{ routeId }/{ username }";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Rating rating = await response.Content.ReadAsAsync<Rating>();

                    return rating;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }
}
