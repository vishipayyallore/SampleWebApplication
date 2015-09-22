using System;
using System.Net;
using System.Net.Mail;
using SendGrid;

namespace copwebapplication.Services
{
    public class Mailer
    {
        private static string from;
        private static string sub;
        private static string bodyHtml;
        private static string username;
        private static string password;
        
        static Mailer()
        {
            from = "reachSajad@gmail.com";
            sub = "Cloud Sample Mail!";
            bodyHtml = "<p>{0}</p>";
            username = "azure_dce1d67beb5656279cff418febdda2f6@azure.com";
            password = "logjvYk2w6vMQb1";
        }

        public static bool SendEmail(string to, string body) 
        {
            SendGridMessage email = new SendGridMessage() 
            {
                From = new MailAddress(from),
                Subject = sub,
                Html = String.Format(bodyHtml, body)
            };

            bool retVal = true;

            try
            {
                email.AddTo(to.Split(new char[] { ',' }));
                SendGrid.Web webTransport = new SendGrid.Web(new NetworkCredential(username, password));
                webTransport.DeliverAsync(email);
            }
            catch
            {
                retVal = false;
            }

            return retVal;
        }
    }
}
