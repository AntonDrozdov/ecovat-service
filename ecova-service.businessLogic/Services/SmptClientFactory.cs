using ecovat_service.businessLogic.Interfaces;
using ecovat_service.businessLogic.Models;
using System.Net;
using System.Net.Mail;

namespace ecovat_service.businessLogic.Services
{
    public class SmtpClientFactory : ISmtpClientFactory
    {
        public SmtpClient CreateSmtpClient(EmailServiceSettings emailServiceSettings)
        {
            var smtpClient = new SmtpClient();

            var credential = new NetworkCredential
            {
                UserName = emailServiceSettings.Username,
                Password = emailServiceSettings.Password
            };
            smtpClient.Credentials = credential;
            smtpClient.Host = emailServiceSettings.Server;
            smtpClient.Port = Convert.ToInt32(emailServiceSettings.Port);
            smtpClient.EnableSsl = true;

            return smtpClient;
        }
    }
}
