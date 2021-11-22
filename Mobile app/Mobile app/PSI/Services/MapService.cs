using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PSI.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using PSI.Views;
using Map = Xamarin.Forms.Maps.Map;

namespace PSI.Services
{
    public class MapService
    {
        private readonly string BaseRouteUrl = "https://router.project-osrm.org/route/v1/driving/";
        private readonly HttpClient _httpClient;
        private readonly Geocoder _geocoder;

        public MapService()
        {
            _httpClient = new HttpClient();
            _geocoder = new Geocoder();
        }

        public async Task<LatLong> GetCoordinatesByAddress(string address)
        {
            IEnumerable<Position> possiblePositions = await _geocoder.GetPositionsForAddressAsync(address);
            Position foundPosition = possiblePositions.FirstOrDefault();
            if (foundPosition == null)
            {
                return null;
            }

            return new LatLong()
            {
                Lat = foundPosition.Latitude,
                Long = foundPosition.Longitude
            };
        }

        public async Task<string> GetAddressByCoordinates(LatLong coords)
        {
            if (coords == null)
            {
                return null;
            }

            IEnumerable<string> possibleAddresses = await _geocoder.GetAddressesForPositionAsync(new Position(coords.Lat, coords.Long));
            return possibleAddresses.FirstOrDefault();
        }

        public async Task<DirectionResponse> GetDirectionResponseAsync(List<VisualWaypoint> coordinates)
        {
            try
            {
                string resultString = "";

                if (coordinates != null)
                {
                    IEnumerable<string> latLongStrings = coordinates.Select(c => c.Long + "," + c.Lat);
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
            List<LatLong> locations = new List<LatLong>();
            List<Leg> legs;
            List<Step> steps;
            List<Intersection> intersections = new List<Intersection>();

            Route route = directionResponse.Routes[0];

            legs = route.Legs.ToList();
            foreach (Leg leg in legs)
            {
                steps = leg.Steps.ToList();

                foreach (var step in steps)
                {
                     List<Intersection> stepIntersections = step.Intersections.ToList();

                    foreach (Intersection intersection in stepIntersections)
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

        public void ResetSingularPin(Map map, bool moveToRegion, Position position)
        {
            map.Pins.Clear();
            Pin newPin = new Pin()
            {
                Type = PinType.Place,
                Label = "Waypoint location",
                Address = "",
                Position = position,
            };
            map.Pins.Add(newPin);

            if (moveToRegion)
            {
                MapSpan mapSpan = MapSpan.FromCenterAndRadius(position, Distance.FromMeters(500));
                map.MoveToRegion(mapSpan);
            }
        }
    }
}
