using ecovat_service.businessLogic.Models;
using System.Net.Mail;

namespace ecovat_service.businessLogic.Interfaces
{
    public interface ISmtpClientFactory
    {
        SmtpClient CreateSmtpClient(EmailServiceSettings emailServiceSettings);
    }
}




