using Automatonymous.Activities;
using Azure.Messaging.ServiceBus;
using System;

namespace fundooNotesCosmos.Services
{
    public class MessageService
    {
       public static void  SendmessgeToQueue(string email, string token)
        {
            ServiceBusClient client = new ServiceBusClient("Endpoint=sb://fundoo-servicebus.servicebus.windows.net/;SharedAccessKeyName=Self;SharedAccessKey=kOfko2cEJsTj5mNj0DvA6GaXipOBPRVQA+ASbCAjTQk=");
            ServiceBusSender sender = client.CreateSender("ResetPassword");
            ServiceBusMessage message = new ServiceBusMessage();
            message.Subject = "";
            message.To = email;
            message.Body = BinaryData.FromString(token);
            sender.SendMessageAsync(message);
        }
    }
}
