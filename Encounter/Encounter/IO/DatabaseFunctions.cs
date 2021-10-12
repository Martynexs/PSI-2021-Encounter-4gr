using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter.IO
{
    public class DatabaseFunctions
    {
        private DatabaseIO _databaseIO = new DatabaseIO();

        public void CreateRoute(string routeName)
        {
            var id = new Random().Next(1000, 9999).ToString();
            var sql = "CREATE TABLE " + routeName + " (ID INT PRIMARY KEY, Name VARCHAR, Longitude DOUBLE, Latitude DOUBLE, Type INT, Price DECIMAL, OpeningHours DATETIME, ClosingTime DATETIME, PhoneNumber  VARCHAR, Description VARCHAR)";
            _databaseIO.ExecuteNonQuery(sql);

            sql = "INSERT INTO Routes(ID, Name, CreatorID) " +
                  "VALUES('" + id + "', '" + routeName + "', 'me')";
            _databaseIO.ExecuteNonQuery(sql);
        }

        public void DeleteRoute(string routeName)
        {
            var sql = "DROP TABLE IF EXISTS '" + routeName + "'";
            _databaseIO.ExecuteNonQuery(sql);

            sql = "DELETE FROM Routes WHERE Name= '" + routeName + "'";
            _databaseIO.ExecuteNonQuery(sql);
        }








    }
}
