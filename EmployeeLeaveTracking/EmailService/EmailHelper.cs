using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace EmailService
{
    public class EmailHelper
    {
        private readonly IConfiguration? _configuration;
        public EmailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(string emailToAddress,string subject, string body)
        {
            string smtpServer = _configuration.GetValue<string>("EmailConfiguration:SmtpServer");
            int port = _configuration.GetValue<int>("EmailConfiguration:Port");
            string from = _configuration.GetValue<string>("EmailConfiguration:From");
            string senderPassword = _configuration.GetValue<string>("EmailConfiguration:Password");
            bool ssl = _configuration.GetValue<bool>("EmailConfiguration:EnableSSL");
            bool htmlBody = _configuration.GetValue<bool>("EmailConfiguration:HtmlBody");

            using MailMessage mail = new();
            string smtpAddress = smtpServer;
            int portNumber = port;
            bool enableSSL = true;
            string emailFromAddress = from; //sender email address  
            string password = senderPassword; //sender password  

            mail.From = new MailAddress(emailFromAddress);
            mail.To.Add(emailToAddress);
            mail.Subject = subject;
            mail.Body = body;                           
            mail.IsBodyHtml = htmlBody;
            //mail.Attachments.Add(new Attachment("C:\\Users\\IncubXperts\\Downloads\\Demo.docx")); //to send any attachment  
            using (SmtpClient smtp = new(smtpAddress, portNumber))
            {
                smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                smtp.EnableSsl = enableSSL;
                smtp.Send(mail);
            }
        }
    }   
}
