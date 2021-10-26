using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Encounter.Converters
{
    [ValueConversion(typeof(Coordinates), typeof(String))]
    public class CoordinatesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var coordinates = (Coordinates)value;
            return coordinates.Latitude.ToString() + ", " + coordinates.Longitude .ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = (string)value;
            var coordinates = stringValue.Split(',');

            try
            {
                var latitude = Double.Parse(coordinates[0]);
                var longitude = Double.Parse(coordinates[1]);
                return new Coordinates(latitude, longitude);
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }
    }
}
