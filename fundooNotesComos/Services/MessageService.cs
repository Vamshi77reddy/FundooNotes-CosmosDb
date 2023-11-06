using Automatonymous.Activities;
using Azure.Messaging.ServiceBus;
using System;
using System.Net.Mail;

namespace fundooNotesCosmos.Services
{
    public class MessageService
    {
        public static void SendmessgeToQueue(string email, string token)
        {
            ServiceBusClient client = new ServiceBusClient("Endpoint=sb://fundoo-servicebus.servicebus.windows.net/;SharedAccessKeyName=Self;SharedAccessKey=kOfko2cEJsTj5mNj0DvA6GaXipOBPRVQA+ASbCAjTQk=");
            ServiceBusSender sender = client.CreateSender("ResetPassword");
            ServiceBusMessage message = new ServiceBusMessage();
            message.Subject = "FundooNotes reset Link";
            message.To = email;
            message.Body = BinaryData.FromString("This is your token for reset the password : " + token);
            sender.SendMessageAsync(message);

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            System.Net.NetworkCredential credential = new System.Net.NetworkCredential("vamshireddy6563@gmail.com", "okjf tftk owbe bsxp");

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = credential;
            MailMessage emailMessage = new MailMessage();
            emailMessage.From = new MailAddress("vamshireddy6563@gmail.com");
            emailMessage.To.Add(email);
            emailMessage.Subject = "FundooNotes reset Link";
            emailMessage.Body = "This is your token for resetting the password: " + token;



            smtpClient.Send(emailMessage);
        }
    }
}
