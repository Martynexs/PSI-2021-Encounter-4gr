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
            var csvPath = Path.Combine(Environment.CurrentDirectory, "C:\\Users\\Vartotojas\\Desktop\\Swashbucklers-Project\\CSV.csv");
            using (var streamWriter = new StreamWriter(csvPath))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";"
                };
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    var waypoints = routeViewModel.GetWaypoints();
                    csvWriter.Context.RegisterClassMap<WaypointClassMap>();
                    csvWriter.WriteRecords(waypoints);
                }
            }
        }

        static public void Read(RouteViewModel routeViewModel)
        {
            using (var streamReader = new StreamReader("C:\\Users\\Vartotojas\\Desktop\\Swashbucklers-Project\\CSV.csv"))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";"
                };
                using (var csvReader = new CsvReader(streamReader, csvConfig))
                {
                    csvReader.Context.RegisterClassMap<WaypointClassMap>();
                    var records = csvReader.GetRecords<Waypoint>();
                }
            }
        }
    }
}
