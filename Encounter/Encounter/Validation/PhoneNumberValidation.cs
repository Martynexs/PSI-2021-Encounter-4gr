using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Encounter.Validation
{
    public class PhoneNumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var phoneNumber = (string)value;
            if(PhoneNumberMatches(phoneNumber))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Invalid phone number");
        }

        private bool PhoneNumberMatches(string phoneNumber)
        {
            var reg = new Regex("^(([+][3][7][0][0-9]{8}) | ([8][0-9]{8}))$");
            bool result = reg.IsMatch(phoneNumber);
            return result;
        }

    }
}
