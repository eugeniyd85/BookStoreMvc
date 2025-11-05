using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMvc.Utility
{
    public class EmailSender : IEmailSender
    {
        //SendGrid integration can be added later, also add appropriate NuGet package
        //public string SendGridSecret { get; set; }

        //public EmailSender(IConfiguration _config)
        //{
        //    SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
        //}

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //SendGrid integration can be added later, also add appropriate NuGet package
            //var client = new SendGridClient(SendGridSecret);

            //var from = new EmailAddress("hello@dotnetmastery.com", "Bulky Book");
            //var to = new EmailAddress(email);
            //var message = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

            //return client.SendEmailAsync(message);

            // logic to send email
            return Task.CompletedTask;
        }
    }
}
