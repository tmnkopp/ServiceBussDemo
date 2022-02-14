using Microsoft.Azure.ServiceBus;
using SBShared.Models;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SBReceiver
{
    class Program
    {
        const string connstr = "";
        const string qname = "personqueue";
        static IQueueClient queueClient;
        static async Task Main(string[] args)
        {
            queueClient = new QueueClient(connstr, qname); 
            var messHandlerOptions = new MessageHandlerOptions(ExceptionHandler) {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messHandlerOptions);
            Console.ReadLine();
            await queueClient.CloseAsync();
        }

        private static async Task ProcessMessagesAsync(Message message, CancellationToken arg2)
        {
            var jsonstring = Encoding.UTF8.GetString(message.Body);
            ReservationModel obj = JsonSerializer.Deserialize<ReservationModel>(jsonstring);
            Console.WriteLine($"recieved { obj.ReservationName} { obj.ReservationIdentifier}");

            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private static Task ExceptionHandler(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine(arg.Exception);
            return Task.CompletedTask;
        }
    }
}
