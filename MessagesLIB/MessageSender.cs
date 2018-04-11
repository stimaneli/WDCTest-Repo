using System;
using System.Text;
using RabbitMQ.Client;

namespace MessagesLIB
{
    public class MessageSender: MessagesHandle, IDisposable
    {
        public MessageSender(MessageOptions messageOptions)
            :base(messageOptions)
        {
            
        }

        public void send(string message)
        {
            // Create exchange
            this.create_exchange();
            // Create queue
            this.create_queue();
            // Bind queue to exchange
            this.bind_queue();
            // Get message bytes
            var body = Encoding.UTF8.GetBytes(message);
            // Send message
            this.Channel.BasicPublish(exchange:this.Exchange, routingKey:this.Binding_Key, basicProperties:null, body:body);
            // Notify
            Console.WriteLine(" [x] Sent: '{0}' to exchange: '{1}'", message, this.Exchange);
        }

        public void Dispose() 
        {
            this.Channel.Close();
            this.Connection.Close();
        }         
    }
}