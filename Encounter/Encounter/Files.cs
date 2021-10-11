using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper;
using System.IO;
using Encounter.ViewModels;

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
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    var waypoints = routeViewModel.GetWaypoints();
                    csvWriter.WriteRecord(waypoints);
                }
            }
        }
    }
}
