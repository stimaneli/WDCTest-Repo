using System;
using RabbitMQ.Client;
using MessagesLIB;

namespace Receive
{
    class Receive
    {
        static void Main(string[] args)
        {
            /// <summary>
            /// Message Options - Required 
            ///    -- Used to interact with the Broker
            ///    -- Default Options setup to work with default settings for Broker
            ///    -- Set properties to match your environment, <see cref="MessageOptions"/>  
            /// </summary>
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
