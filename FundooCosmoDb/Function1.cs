using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
 
namespace FundooCosmoDb
{
    public class Function1
    {
        [FunctionName("Function1")]
        public static void Run([ServiceBusTrigger("ResetPassword", Connection = "Fundoo")]string body,string to,string label, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {body+to+label}");
        }
    }
}
