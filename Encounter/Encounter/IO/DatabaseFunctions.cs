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
                var id = new Random().Next(1000, 9999).ToString();
                var sql = "CREATE TABLE " + route.Name + " (ID INT PRIMARY KEY, Name VARCHAR, Longitude DOUBLE, Latitude DOUBLE, Type VARCHAR, Price DECIMAL, OpeningHours DATETIME, ClosingTime DATETIME, PhoneNumber  VARCHAR, Description VARCHAR)";
                DatabaseIO.ExecuteNonQuery(sql);

                sql = "INSERT INTO Routes(ID, Name, CreatorID) " +
                      "VALUES('" + id + "', '" + route.Name + "', '" + route.CreatorID + "')";
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
                var sql = "DROP TABLE IF EXISTS '" + route.Name + "'";
                DatabaseIO.ExecuteNonQuery(sql);

                sql = "DELETE FROM Routes WHERE Name= '" + route.Name + "'";
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
                DeleteRoute(route);
                CreateRoute(route);
                foreach (var waypoint in waypoints)
                {
                    var sql = "INSERT INTO '" + route.Name + "' (ID, Name, Longitude, Latitude, Type, Price, OpeningHours, ClosingTime, PhoneNumber, Description) " +
                              "VALUES('" + waypoint.Index + "', '" + waypoint.Name + "', '" + waypoint.Coordinates.Longitude + "', '" + waypoint.Coordinates.Latitude + "', '" + waypoint.Type + "', '" + waypoint.Price + "', '" + waypoint.OpeningHours + "', '" + waypoint.ClosingTime + "', '" + waypoint.PhoneNumber + "', '" + waypoint.Description + "')";
                    DatabaseIO.ExecuteNonQuery(sql);
                }
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
                    route.ID = reader["ID"].ToString();

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

                var sql = "SELECT * FROM '" + route.Name + "'";
                var reader = DatabaseIO.GetDataReader(sql);

                while (reader.Read())
                {
                    var waypoint = new Waypoint();
                    waypoint.Index = (int)reader["ID"];
                    waypoint.Name = reader["Name"].ToString();
                    var latitude = (double)reader["Latitude"];
                    var longitude = (double)reader["Longitude"];
                    waypoint.Coordinates = new Coordinates(latitude, longitude);
                    waypoint.Type = (WaypointType)Enum.Parse(typeof(WaypointType), reader["Type"].ToString());


                    //waypoint.OpeningHours = DateTime.ParseExact(reader["OpeningHours"].ToString(), "yyyy-MM-dd hh:mm:ss tt", null);
                                                                                                  //0001-01-01 12:00:00 AM
                   // waypoint.ClosingTime = DateTime.ParseExact(reader["ClosingTime"].ToString(), "yyyy-MM-dd hh:mm:ss tt", null);
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
