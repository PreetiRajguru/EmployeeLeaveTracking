using System.Net.Mail;
using System.Net;

namespace EmailService
{
    public class EmailHelper
    {

        public void SendEmail(string emailToAddress,string subject, string body)
        {
            using MailMessage mail = new();
            string smtpAddress = "smtp.gmail.com";
            int portNumber = 587;
            bool enableSSL = true;
            string emailFromAddress = "preetyraj340@gmail.com"; //sender email address  
            string password = "bjnuvxfkcbwqmmye"; //sender password  

            mail.From = new MailAddress(emailFromAddress);
            mail.To.Add(emailToAddress);
            mail.Subject = subject;
            mail.Body = body;                           
            mail.IsBodyHtml = true;
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
