using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PSI.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Map3.Views;
using PSI.Services;
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

        public List<LatLong> ExtractLocations(DirectionResponse dr)
        {
            List<LatLong> locations = new List<LatLong>();
            Route route;
            List<Leg> legs;
            List<Step> steps = new List<Step>();
            List<Intersection> intersections = new List<Intersection>();

            route = dr.Routes[0];

            legs = route.Legs.ToList();
            foreach (Leg leg in legs)
            {
                steps.AddRange(leg.Steps.ToList());

                foreach (Step step in steps)
                {
                    List<Intersection> localIntersections = step.Intersections.ToList();

                    foreach (Intersection intersection in localIntersections)
                    {
                        intersections.Add(intersection);
                    }
                }
            }
            foreach (Intersection intersection in intersections)
            {
                LatLong p = new LatLong
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
            foreach (VisualWaypoint item in visualWaypoints)
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
