using ecovat_service.businessLogic.Models;

namespace ecovat_service.businessLogic.Interfaces
{
    public interface IEmailService
    {
        Task<bool> Send(Message message);
    }
}
