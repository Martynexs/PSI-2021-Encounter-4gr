using System.Threading.Tasks;
using Map3.Models;
using Map3.ViewModels;
using Map3.Views;
using System.Collections.ObjectModel;
using System.Linq;
using Map3.Services;
using Newtonsoft.Json;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using System.Net.Http;


namespace Map3
{
    public class RouteServiceBase
    {

        private readonly string BaseRouteUrl = "https://router.project-osrm.org/route/v1/driving";
        private HttpClient _httpClient;

        public RouteServiceBase()
        {
            _httpClient = new HttpClient();
        }


        public async Task<DirectionResponse> GetDirectionResponseAsync(string origin, string destination)
        {
            try
            {
                var originLocations = await Geocoding.GetLocationsAsync(origin);
                var originLocation = originLocations?.FirstOrDefault();
                var destinationLocations = await Geocoding.GetLocationsAsync(destination);
                var destinationLocation = destinationLocations?.FirstOrDefault();

                if (originLocation == null || destinationLocation == null)
                {
                    return null;
                }
                if (originLocation != null || destinationLocation != null)
                {
                    string url = string.Format(BaseRouteUrl) + $"{originLocation.Latitude}, {originLocation.Longitude};" +
                    $"{destinationLocation.Latitude}, {destinationLocation.Longitude}?overview=full&geometries=polylines&steps=true";

                    var response = await _httpClient.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
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
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}