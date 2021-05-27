using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace STARK_Project.EmailServices
{
    public class SMTPService : IEmailService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public SMTPService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task SendEmail(string senderEmail, string recipientEmail,string title, string message)
        {
            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = _hostingEnvironment.WebRootPath + @"\Emails"
        });

            Email.DefaultSender = sender;

            var email = await Email.From(senderEmail)
                .To(recipientEmail)
                .Subject(title)
                .Body(message)
                .SendAsync();
        }
    }
}
