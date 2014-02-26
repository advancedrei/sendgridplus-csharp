using SendGrid;
using SendGrid.Transport;
using System;
using System.Net;
using System.Net.Mail;

namespace Example
{
    class Program
    {
        // this code is used for the SMTPAPI examples
        static void Main(string[] args)
        {
            // Create the email object first, then add the properties.
            Mail myMessage = Mail.GetInstance();
            myMessage.AddTo("anna@example.com");
            myMessage.From = new MailAddress("john@example.com", "John Smith");
            myMessage.Subject = "Testing the SendGrid Library";
            myMessage.Text = "Hello World!";
            myMessage.Html = "<html><body><p>Hello World</p><img src=\"cid:img1\"/></body></html>";

            var imgStream = System.IO.File.OpenRead(@"img_test.png");
            myMessage.AddAttachment(imgStream, "img_test.png", "image/png", "img1");

            // Create credentials, specifying your user name and password.
            var credentials = new NetworkCredential("username", "password");

            // Create a Web transport for sending email.
            var transportWeb = Web.GetInstance(credentials);

            // Send the email.
            transportWeb.DeliverAsync(myMessage);

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

    }
}
