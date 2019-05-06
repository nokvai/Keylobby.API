using Keylobby.API.DataAccessLayer.Models;
using Keylobby.API.Identity.BusinessLogicLayer.Interface;
using Keylobby.API.Identity.DataAccessLayer.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Keylobby.API.Identity.BusinessLogicLayer.Service
{
    public class AuthMessageSender : IEmailSender
    {
        public AuthMessageSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public EmailSettings _emailSettings { get; }

        public Task SendEmailAsync(string message)
        {
            Execute(message).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(string message)
        {
            var template = $"HtmlTemplate/EmailTemplate.html";
            ContactUsModel contactUsObj = JsonConvert.DeserializeObject<ContactUsModel>(message);
            var emailMessage = new MimeMessage
            {
                From = {
                    new MailboxAddress("Keylobby Team", _emailSettings.UsernameEmail)
                },
                To = {
                    new MailboxAddress("Recipient", _emailSettings.UsernameEmail)
                },
                Subject = "New Keylobby Inquiry",
                Body = new BodyBuilder()
                {
                    HtmlBody = File.ReadAllText(template).Replace("<Name>", contactUsObj.Name)
                                                             .Replace("<Email>", contactUsObj.Email)
                                                             .Replace("<Phone>", contactUsObj.Phone)
                                                             .Replace("<Message>", contactUsObj.Message)
                }.ToMessageBody()
            };
            
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTlsWhenAvailable).ConfigureAwait(false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            };
        }



    }
}
