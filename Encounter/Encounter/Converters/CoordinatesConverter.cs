using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Encounter.Converters
{
    [ValueConversion(typeof(Coordinates), typeof(String))]
    public class CoordinatesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Coordinates coordinates = (Coordinates)value;
            return coordinates.latitude.ToString() + ", " + coordinates.longitude .ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringValue = (string)value;

            var coords = stringValue.Split(',');

            try
            {
                var latitude = Double.Parse(coords[0]);
                var longitude = Double.Parse(coords[1]);
                return new Coordinates(latitude, longitude);
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }
    }
}
