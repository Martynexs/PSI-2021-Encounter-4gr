using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper;
using System.IO;
using Encounter.ViewModels;
using CsvHelper.Configuration;

namespace Encounter
{
    [Serializable]
    static class Files
    {
        static public void Write(RouteViewModel routeViewModel)
        {
            var path = FileController.CreateFile();
            using (var streamWriter = new StreamWriter(path))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";"
                };
                using (var csvWriter = new CsvWriter(streamWriter, csvConfig))
                {
                    var waypoints = routeViewModel.GetWaypoints();
                    csvWriter.Context.RegisterClassMap<WaypointClassMap>();
                    csvWriter.WriteRecords(waypoints);
                }
            }
        }

        static public IEnumerable<Waypoint> Read()
        {
            IEnumerable<Waypoint> waypoints;
            var path = FileController.SelectFile();
            try
            {
                using (var streamReader = new StreamReader(path))
                {
                    var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ";"
                    };
                    using (var csvReader = new CsvReader(streamReader, csvConfig))
                    {
                        csvReader.Context.RegisterClassMap<WaypointClassMap>();
                        var records = csvReader.GetRecords<Waypoint>();
                        waypoints = records.ToList();
                    }
                }
                return waypoints;
            }
            catch (System.NullReferenceException)
            {
                return null;
            }
            catch(System.ArgumentNullException)
            {
                return null;
            }
        }
    }
}
