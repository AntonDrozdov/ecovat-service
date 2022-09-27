using ecovat_service.businessLogic.Interfaces;
using ecovat_service.businessLogic.Models;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace ecovat_service.businessLogic.Services
{
    public class EmailService : IEmailService
    {
        private ISmtpClientFactory _smtpClientFactory;
        //private SmtpClient _smptClient { get; }

        private EmailServiceSettings _emailServiceSettings;

        public EmailService(ISmtpClientFactory smtpClientFactory, IOptionsMonitor<EmailServiceSettings> optionsMonitor)
        {
            _emailServiceSettings = optionsMonitor.CurrentValue;

            _smtpClientFactory = smtpClientFactory;
            //_smptClient = _smtpClientFactory.CreateSmtpClient(_emailServiceSettings);
        }

        public async Task<bool> Send(Message message)
        {
            try
            {
               using var smtpClient = new SmtpClient();
                await smtpClient.ConnectAsync(_emailServiceSettings.Server, Convert.ToInt32(_emailServiceSettings.Port), SecureSocketOptions.Auto);

                await smtpClient.AuthenticateAsync(_emailServiceSettings.Username, _emailServiceSettings.Password);

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress("SITE:ECOVAT-SERVICE.RU", _emailServiceSettings.Username));

                foreach (var address in _emailServiceSettings.MailTo)
                    mimeMessage.To.Add(new MailboxAddress(address, address));

                var builder = new BodyBuilder { TextBody = message.Body };
                mimeMessage.Body = builder.ToMessageBody();
                mimeMessage.Subject = message.Subject;

                await smtpClient.SendAsync(mimeMessage);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    } 
}
