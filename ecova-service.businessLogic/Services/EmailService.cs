using ecovat_service.businessLogic.Interfaces;
using ecovat_service.businessLogic.Models;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace ecovat_service.businessLogic.Services
{
    public class EmailService : IEmailService
    {
        private ISmtpClientFactory _smtpClientFactory; 
        private SmtpClient _smptClient { get; }

        private EmailServiceSettings _emailServiceSettings;

        public EmailService(ISmtpClientFactory smtpClientFactory, IOptionsMonitor<EmailServiceSettings> optionsMonitor)
        {
            _emailServiceSettings = optionsMonitor.CurrentValue;

            _smtpClientFactory = smtpClientFactory;
            _smptClient = _smtpClientFactory.CreateSmtpClient(_emailServiceSettings);
        }

        public bool Send(Message message)
        {
            {
                try
                {
                    using (MailMessage mailMessage = CreateMailMessage(message.Subject, message.Body))
                    {
                       _smptClient.Send(mailMessage);
                    }

                }
                catch (Exception ex)
                {
                    return false;
                }

                return true;
            }
        }

        private MailMessage CreateMailMessage(string subject, string body)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("SITE@ECOVAT-SERVICE.RU");

            foreach (var mailTo in _emailServiceSettings.MailTo)
            {
                mail.To.Add(new MailAddress(mailTo));
            }

            mail.Subject = subject;
            mail.Body = body;

            return mail;
        }
    }
}
