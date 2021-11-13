using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Essentials;
using PSI.Models;
using PSI.Views;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Map3.Views;
using PSI.Services;

namespace Map3
{
    public class MapService
    {
        private readonly string BaseRouteUrl = "https://router.project-osrm.org/route/v1/driving/";
        private readonly HttpClient _httpClient;
        private WaypointsCoordinatesService waypointsCoordinatesService = new WaypointsCoordinatesService();

        public MapService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<DirectionResponse> GetDirectionResponseAsync(List<VisualWaypoint> coordinates)
        {
            try
            {
                    List<string> latLongStrings = new List<string>();
                    string resultString = "";

                    if (coordinates != null)
                    {
                        foreach(VisualWaypoint coordinate in coordinates)
                        {
                            latLongStrings.Add(coordinate.Long + "," + coordinate.Lat);
                        }
                        resultString = string.Join(";", latLongStrings.ToArray());
                    }

                    string url = string.Format(BaseRouteUrl) + resultString + "?overview=full&steps=true";

                    HttpResponseMessage response = await _httpClient.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        DirectionResponse result = JsonConvert.DeserializeObject<DirectionResponse>(json);
                        return result.Code.Equals("Ok") ? result : null;
                    }
                return null;
                
             
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
