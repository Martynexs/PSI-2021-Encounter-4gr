using Encounter.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Encounter.IO
{
    public static class DatabaseFunctions
    {
        public static void CreateRoute(Route route)
        {
            try
            {
                var sql = "INSERT INTO Routes(ID, Name, CreatorID) " +
                          "VALUES('" + route.ID + "', '" + route.Name + "', '" + route.CreatorID + "')";
                DatabaseIO.ExecuteNonQuery(sql);
            }
            catch
            {
                throw new Exception();
            }
        }

        public static void DeleteRoute(Route route)
        {
            try
            {
                var sql = "DELETE FROM Routes WHERE ID= '" + route.ID + "'";
                DatabaseIO.ExecuteNonQuery(sql);
            }
            catch
            {
                throw new Exception();
            }
        }

        public static void SaveRoute(Route route, IEnumerable<Waypoint> waypoints)
        {
            try
            {
                var sql = "DELETE FROM Waypoints WHERE RouteID='" + route.ID + "';\n";
                foreach (var waypoint in waypoints)
                {
                    sql += "INSERT INTO Waypoints (RouteID, Position, Name, Longitude, Latitude, Type, Price, OpeningHours, ClosingTime, PhoneNumber, Description) " +
                           "VALUES('" + route.ID + "', '" + waypoint.Index + "', '" + waypoint.Name + "', '" + waypoint.Coordinates.Longitude + "', '" + waypoint.Coordinates.Latitude + "', '" + waypoint.Type + "', '" + waypoint.Price + "', '" + waypoint.OpeningHours.ToString("HH:mm") + "', '" + waypoint.ClosingTime.ToString("HH:mm") + "', '" + waypoint.PhoneNumber + "', '" + waypoint.Description + "');\n";
                }
                DatabaseIO.ExecuteNonQuery(sql);
            }
            catch
            {
                throw new Exception();
            }
        }

        public static ObservableCollection<Route> GetRoutes()
        {
            try
            {
                var routeList = new ObservableCollection<Route>();
                var sql = "SELECT * FROM Routes";
                var reader = DatabaseIO.GetDataReader(sql);
                
                while(reader.Read())
                {
                    var route = new Route();
                    route.Name = reader["Name"].ToString();
                    route.CreatorID = reader["CreatorID"].ToString();
                    route.ID = (int)reader["ID"];

                    routeList.Add(route);
                }

                return routeList;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw new Exception();
            }
        }

        public static List<Waypoint> GetWaypoints(Route route)
        {
            try
            {
                var waypoints = new List<Waypoint>();

                var sql = "SELECT * FROM Waypoints WHERE RouteID = '" + route.ID + "' ORDER BY Position";
                var reader = DatabaseIO.GetDataReader(sql);

                while (reader.Read())
                {
                    var waypoint = new Waypoint();
                    waypoint.Index = (int)reader["Position"];
                    waypoint.Name = reader["Name"].ToString();
                    var latitude = (double)reader["Latitude"];
                    var longitude = (double)reader["Longitude"];
                    waypoint.Coordinates = new Coordinates(latitude, longitude);
                    waypoint.Type = (WaypointType)Enum.Parse(typeof(WaypointType), reader["Type"].ToString());


                    waypoint.OpeningHours = DateTime.ParseExact(reader["OpeningHours"].ToString(), "HH:mm", null);
                    waypoint.ClosingTime = DateTime.ParseExact(reader["ClosingTime"].ToString(), "HH:mm", null);
                    waypoint.PhoneNumber = reader["PhoneNumber"].ToString();
                    waypoint.Description = reader["Description"].ToString();
                    waypoint.Price = (decimal)reader["Price"];

                    waypoints.Add(waypoint);
                }
                return waypoints;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw new Exception();
            }
        }
    }
}
