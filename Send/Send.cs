using System;
using RabbitMQ.Client;
using MessagesLIB;

namespace Send
{
    class Send
    {
        static void Main(string[] args)
        {
            // Message
            string message = "";
            // Condition to send message
            bool send_msg_condition;   
            // Message Options
            var message_options = new MessageOptions();  

            Console.WriteLine(" --- Welcome, when done input exit to exit ---");

            using (var sender = new MessageSender(message_options))
            {
                do
                {
                    // Print
                    Console.WriteLine();
                    Console.Write("Please input name: ");
                    
                    // Get name  
                    message = Console.ReadLine();   


                    // Set condition to send message: send message if name has value and value not exit
                    send_msg_condition = !String.IsNullOrEmpty(message) && !message.Equals("exit", StringComparison.CurrentCultureIgnoreCase);

                    // Condition to send message met
                    if (send_msg_condition)
                    {
                        sender.send(String.Format("Hello my name is, {0}", message));                        
                    }

                } while (!message.Equals("exit", StringComparison.CurrentCultureIgnoreCase));
            }
        }
    }
}
