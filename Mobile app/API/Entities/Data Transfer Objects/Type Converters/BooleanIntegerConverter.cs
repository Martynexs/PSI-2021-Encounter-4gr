using System;

namespace Entities.Data_Transfer_Objects.TypeConverters
{
    public class BooleanIntegerConverter
    {
        private static readonly Lazy<BooleanIntegerConverter> _booleanIntegerConverter =
            new Lazy<BooleanIntegerConverter>(() => new BooleanIntegerConverter());

        public static BooleanIntegerConverter Instance { get => _booleanIntegerConverter.Value; }
        private BooleanIntegerConverter()
        {
        }

        public bool IntegerToBool(int value)
        {
            return value != 0 ? true : false;
        }

        public int BoolToInteger(bool value)
        {
            return value ? 1 : 0;
        }
    }
}
