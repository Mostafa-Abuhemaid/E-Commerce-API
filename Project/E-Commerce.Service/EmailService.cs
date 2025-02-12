using E_Commerce.Core.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Service
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_mailSettings.Email, _mailSettings.DisplayName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            using var smtpClient = new SmtpClient(_mailSettings.Host)
            {
                Port = _mailSettings.Port,
                Credentials = new NetworkCredential(_mailSettings.Email, _mailSettings.Password),
                EnableSsl = _mailSettings.EnableSSL,
                UseDefaultCredentials = _mailSettings.UseDefaultCredentials
            };

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
    

}   

