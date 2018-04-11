using System;
using RabbitMQ.Client;
using MessagesLIB;

namespace Receive
{
    class Receive
    {
        static void Main(string[] args)
        {
            // Message Options
            var message_options = new MessageOptions();  

            Console.WriteLine(" --- Welcome, waiting for messages ---");
            Console.WriteLine();

            using (var receiver = new MessageReceiver(message_options))
            {
                // Print messages
                receiver.Receive();
                
                Console.WriteLine("Press [enter] to exit.");
                Console.ReadLine();                  
            }
        }
    }
}
