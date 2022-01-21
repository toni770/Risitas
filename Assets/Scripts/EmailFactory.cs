using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using TMPro;

public class EmailFactory : MonoBehaviour
{

    public TextMeshProUGUI errorText;
    public void SendEmail(string SmtpClient, int port, string user, string pass, string to, string subject, string body)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(SmtpClient);
            SmtpServer.Timeout = 10000;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.Port = port;

            mail.From = new MailAddress(user);
            mail.To.Add(new MailAddress(to));

            mail.Subject = subject;
            mail.Body = body;


            SmtpServer.Credentials = new System.Net.NetworkCredential(user, pass); 

            SmtpServer.UseDefaultCredentials = false;

            SmtpServer.EnableSsl = true;

            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            string userState = "test message1";
            SmtpServer.SendAsync(mail,userState);
            SmtpServer.Dispose();

        }
        catch (Exception e)
        {
            errorText.text = e.ToString();
            throw new Exception(e.ToString());
        }
        
    }

    public void SendText(string phoneNumber)
    {
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        SmtpServer.Timeout = 10000;
        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        SmtpServer.UseDefaultCredentials = false;

        mail.From = new MailAddress("myEmail@gmail.com");

        mail.To.Add(new MailAddress(phoneNumber + "@txt.att.net"));//See carrier destinations below
                                                                   //message.To.Add(new MailAddress("5551234568@txt.att.net"));
        mail.To.Add(new MailAddress(phoneNumber + "@vtext.com"));
        mail.To.Add(new MailAddress(phoneNumber + "@messaging.sprintpcs.com"));
        mail.To.Add(new MailAddress(phoneNumber + "@tmomail.net"));
        mail.To.Add(new MailAddress(phoneNumber + "@vmobl.com"));
        mail.To.Add(new MailAddress(phoneNumber + "@messaging.nextel.com"));
        mail.To.Add(new MailAddress(phoneNumber + "@myboostmobile.com"));
        mail.To.Add(new MailAddress(phoneNumber + "@message.alltel.com"));
        mail.To.Add(new MailAddress(phoneNumber + "@mms.ee.co.uk"));



        mail.Subject = "Subject";
        mail.Body = "";

        SmtpServer.Port = 587;

        SmtpServer.Credentials = new System.Net.NetworkCredential("myEmail@gmail.com", "MyPasswordGoesHere") as ICredentialsByHost; SmtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };

        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        SmtpServer.Send(mail);
    }
}