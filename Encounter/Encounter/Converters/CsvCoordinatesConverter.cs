using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

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
            var coordinates = (Coordinates)value;
            return coordinates.Latitude.ToString() + ", " + coordinates.Longitude.ToString();
        }
    }
}
