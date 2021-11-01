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
        private readonly string BaseRouteUrl = "http://router.project-osrm.org/route/v1/driving/";
        private HttpClient _httpClient;

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
                    $"{destinationLocation.Longitude},{destinationLocation.Latitude}?overview=full&geometries=polyline&steps=true";

                    var response = await _httpClient.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();

                    if(response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<DirectionResponse>(json);
                        if (result.Code.Equals("Ok"))
                        {
                            return result;

                        }
                       

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
