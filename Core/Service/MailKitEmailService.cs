using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MailKitEmailService(IConfiguration _configuration) : IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string htmlBody)
        {
          var msg = new MimeMessage();
            msg.From.Add(MailboxAddress.Parse(_configuration["SMTP:From"]));
            msg.To.Add(MailboxAddress.Parse(to));
            msg.Subject = subject;
            msg.Body = new BodyBuilder { HtmlBody = htmlBody }.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
           await smtp.ConnectAsync(_configuration["SMTP:Host"], int.Parse(_configuration["SMTP:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
           await smtp.AuthenticateAsync(_configuration["SMTP:User"], _configuration["SMTP:Pass"]);
           await smtp.SendAsync(msg);
           await smtp.DisconnectAsync(true);
        }
    }
}
