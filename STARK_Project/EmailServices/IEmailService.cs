using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.EmailServices
{
    interface IEmailService
    {
        Task SendEmail(string senderEmail, string recipientEmail,string title, string message);
    }
}
