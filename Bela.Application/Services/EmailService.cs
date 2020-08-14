using Bela.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Bela.Application.Utility;

namespace Bela.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            EmailConfigModel emailConfig = _config.GetSection("EmailSmtpConfig").Get<EmailConfigModel>();
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(emailMessage.From));
            message.To.Add(MailboxAddress.Parse(emailMessage.To));
            message.Subject = emailMessage.Subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = emailMessage.Body
            };

            using var emailClient = new SmtpClient();
            emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
            await emailClient.ConnectAsync(emailConfig.SmtpServer, emailConfig.SmtpPort, false).ConfigureAwait(false);
            await emailClient.AuthenticateAsync(emailConfig.SmtpUsername, emailConfig.SmtpPassword).ConfigureAwait(false);
            await emailClient.SendAsync(message).ConfigureAwait(false);
            await emailClient.DisconnectAsync(true).ConfigureAwait(false);
        }
    }
}
