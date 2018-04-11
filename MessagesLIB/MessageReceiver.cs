using System;
using System.Text;
using System.Text.RegularExpressions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessagesLIB
{
    public class MessageReceiver: MessagesHandle, IDisposable
    {
        private byte[] body;
        private string message;
        // EventingBasicConsumer consumer;

        public MessageReceiver(MessageOptions messageOptions)
            :base(messageOptions)
        {
            
        }

        public void Receive()
        {
            // Create exchange
            this.create_exchange();
            // Create queue
            this.create_queue();
            // Bind queue to exchange
            this.bind_queue();
            // Init message consumer
            var consumer = new EventingBasicConsumer(this.Channel);
            consumer.Received += (model, ea) =>
            {
                body = ea.Body;
                message = Encoding.UTF8.GetString(body);
                // Extract name
                var name = message.Substring(message.LastIndexOf(',')+1).Trim();
                // Print
                PrintMessage(name);
                // Acknowledge received
                this.Channel.BasicAck(ea.DeliveryTag, false);
            };
            // Listen for messages
            this.Channel.BasicConsume(queue: this.Queue_name, autoAck: false, consumer: consumer);
        }

        private bool IsAlphabetic(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z ]+$");
        }

        private void PrintMessage(string name)
        {
            if (IsAlphabetic(name))
            {
                Console.WriteLine("Hello {0}, I am your father!", name);
            }
            else
            {
                Console.WriteLine("Invalid name type. Name '{0}', not processed", name);
            }
        }

        public void Dispose() 
        {
            this.Channel.Close();
            this.Connection.Close();
        }         
    }
}