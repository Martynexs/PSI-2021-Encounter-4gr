using System;

namespace Encounter.Converters
{
    public static class CurrencyConverter
    {
        public static double ConvertCurrency(decimal price, string currencyFrom, string currencyTo)
        {
            double rate;
            if (currencyFrom == "USD" && currencyTo == "EUR")
            {
                rate = 1 / 1.569;
            }
            else if (currencyFrom == "EUR" && currencyTo == "USD")
            {
                rate = 1.569;
            }
            else if (currencyFrom == "GBP" && currencyTo == "EUR")
            {
                rate = 1 / 0.84890;
            }
            else if (currencyFrom == "EUR" && currencyTo == "GBP")
            {
                rate = 0.84890;
            }
            else
            {
                throw new ArgumentException("Can't convert from " + currencyFrom + " to " + currencyTo);
            }
            return ((double)price) * rate;
        }
    }
}
