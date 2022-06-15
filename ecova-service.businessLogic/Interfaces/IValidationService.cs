namespace ecovat_service.businessLogic.Interfaces
{
    public interface IValidationService
    {
        bool ValidateFeedbackForm(string name, string email, string phone, string message);
    }
}
