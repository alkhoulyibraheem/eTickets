using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public async Task Send(string to, string subject, string body)
        {
			var message = new MailMessage();

			message.From = new MailAddress("albbayrbl@gmail.com", "eTcket App");
			message.Subject = subject;
			message.Body = body;
			message.To.Add(new MailAddress(to));
			message.IsBodyHtml = false;

			var emailClient = new SmtpClient
			{
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential("albbayrbl@gmail.com", "blvwthkexeisujut")
			};


			await emailClient.SendMailAsync(message);

		}
    }
}
