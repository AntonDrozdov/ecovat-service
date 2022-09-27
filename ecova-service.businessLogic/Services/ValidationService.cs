using System.Text.RegularExpressions;
using ecovat_service.businessLogic.Interfaces;

namespace ecovat_service.businessLogic.Services
{
    public class ValidationService : IValidationService
    {
        public bool ValidateFeedbackForm(string name, string email, string phone, string message)
        {
            var nameValid = name.Length >= 2;

            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            var emailValid = Regex.Match(email, pattern, RegexOptions.IgnoreCase).Success;

            //pattern = "^[0-9]{3,15}$";
            //var telephonValid = Regex.Match(phone, pattern, RegexOptions.IgnoreCase).Success;

            var messageValid = message.Length >= 10 && message.Length <= 3000;

            return nameValid && emailValid && messageValid;
        }
    }
}
