using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter.Converters
{
    public class CsvCoordinatesConverter : ITypeConverter
    {
        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {

            var coords = text.Split(',');

            try
            {
                var latitude = Double.Parse(coords[0]);
                var longitude = Double.Parse(coords[1]);
                return new Coordinates(latitude, longitude);
            }
            catch
            {
                throw new Exception();
            }
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            Coordinates coordinates = (Coordinates)value;
            return coordinates.latitude.ToString() + ", " + coordinates.longitude.ToString();
        }
    }
}
