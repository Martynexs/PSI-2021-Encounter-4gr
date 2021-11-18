using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PSI.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Map3.Views;
using Xamarin.Forms.Maps;
using Xamarin.Forms;

namespace Map3
{
    public class MapService
    {
        private readonly string BaseRouteUrl = "https://router.project-osrm.org/route/v1/driving/";
        private readonly HttpClient _httpClient;

        public MapService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<DirectionResponse> GetDirectionResponseAsync(List<VisualWaypoint> coordinates)
        {
            try
            {
                string resultString = "";

                if (coordinates != null)
                {
                    var latLongStrings = coordinates.Select(c => c.Long + "," + c.Lat);
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

        public List<LatLong> ExtractLocations(DirectionResponse directionResponse)
        {
            var locations = new List<LatLong>();
            var legs = new List<Leg>();
            var steps = new List<Step>();
            var intersections = new List<Intersection>();

            var route = directionResponse.Routes[0];

            legs = route.Legs.ToList();
            foreach (var leg in legs)
            {
                steps.AddRange(leg.Steps.ToList());

                foreach (var step in steps)
                {
                    var localIntersections = step.Intersections.ToList();

                    foreach (var intersection in localIntersections)
                    {
                        intersections.Add(intersection);
                    }
                }
            }
            foreach (var intersection in intersections)
            {
                var p = new LatLong
                {
                    Lat = intersection.Location[1],
                    Long = intersection.Location[0]
                };
                locations.Add(p);
            }

            return locations;
        }

        public void DrawPins(List<VisualWaypoint> visualWaypoints, Map map)
        {
            foreach (var item in visualWaypoints)
            {
                Pin WaypointPins = new Pin()
                {
                    Type = PinType.Place,
                    Label = item.Name,
                    Address = item.Description,
                    Position = new Position(item.Lat, item.Long),
                };
                map.Pins.Add(WaypointPins);
            }
        }

        public void DrawPolyline(List<LatLong> locations, Map map)
        {
            Polyline polyline = new Polyline
            {
                StrokeColor = Color.Blue,
                StrokeWidth = 9,
            };

            foreach (LatLong latlong in locations)
            {
                polyline.Geopath.Add(new Position(latlong.Lat, latlong.Long));
            }
            map.MapElements.Add(polyline);
        }
    }
}
