using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Map3.Models;
using Map3.Views;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace Map3
{
     public class RouteService
    {
        private readonly string BaseRouteUrl = "https://router.project-osrm.org/route/v1/driving/";
        private readonly HttpClient _httpClient;

        public RouteService()
        {
            _httpClient = new HttpClient();
        }


        public async Task <DirectionResponse> GetDirectionResponseAsync(string origin, string destination)
        {
            try
            {
                var originLocations = await Geocoding.GetLocationsAsync(origin);
                var originLocation = originLocations?.FirstOrDefault();
                var destinationLocations = await Geocoding.GetLocationsAsync(destination);
                var destinationLocation = destinationLocations?.FirstOrDefault();

                if(originLocation == null || destinationLocation == null)
                {
                    return null;
                }
                if (originLocation != null && destinationLocation != null)
                {
                    string url = string.Format(BaseRouteUrl) + $"{originLocation.Longitude},{originLocation.Latitude};" +
                     $"{destinationLocation.Longitude},{destinationLocation.Latitude}?overview=full&steps=true";

                    var response = await _httpClient.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    //var json = "";
                    // var json = "{\"code\":\"Ok\",\"waypoints\":[{\"hint\":\"P9wkgcKzIYwFAAAAAgAAAAAAAAAHAAAAFFi_QJLUDEAAAAAA2wzxQAUAAAACAAAAAAAAAAcAAAAx5QAAmLyBAZ51QgOjvIEBtHVCAwAAnwZWl_ws\",\"distance\":2.549754,\"location\":[25.27964,54.687134],\"name\":\"Gedimino pr.\"},{\"hint\":\"k7gbkNG4G5AEAAAAAAAAAAkAAAAAAAAA5ZO7PwAAAADVN3JAAAAAAAQAAAAAAAAACQAAAAAAAAAx5QAAvOQjAJuA6QJe5CMAJn7pAgEAXw1Wl_ws\",\"distance\":70.288429,\"location\":[2.352316,48.857243],\"name\":\"\"}],\"routes\":[{\"legs\":[{\"steps\":[],\"weight\":78609.8,\"distance\":2021090.6,\"summary\":\"\",\"duration\":78597.1}],\"weight_name\":\"routability\",\"weight\":78609.8,\"distance\":2021090.6,\"duration\":78597.1}]}";

                    //if (response.IsSuccessStatusCode)
                    if (true)
                    {
                        var result = JsonConvert.DeserializeObject<DirectionResponse>(json);
                        if (result.Code.Equals("Ok"))
                        {
                            return result;

                        }
                        return null;
                       

                    }
                } 
                else
                {
                    return null;
                }

                return null;

            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
