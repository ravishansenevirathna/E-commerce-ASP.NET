using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EcommerceApi.Config
{
    public class EmailSender
     {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendOtpEmailAsync(string toEmail, string otpCode)
        {
            // Retrieve email settings from configuration
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var senderPassword = _configuration["EmailSettings:SenderPassword"];

            // Set up the SMTP client
            var smtpClient = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
            };

            // Create the email message
            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = "Your OTP Code",
                Body = $"Your OTP code is: {otpCode}",
                IsBodyHtml = false,
            };

            mailMessage.To.Add(toEmail);

            // Send the email asynchronously
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}