using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace MessagesLIB
{
    public abstract class MessagesHandle
    {
        // ConnectionFactory
        private ConnectionFactory factory;
        
        // Connection
        private IConnection connection;
        protected IConnection Connection
        {
            get { return connection;}
        }
        
        // Channel
        private IModel channel;
        protected IModel Channel
        {
            get { return channel;}
        }
        
        // Queue name
        private string queue_name;
        protected string Queue_name
        {
            get { return queue_name;}
        }         
        // Exchange
        private string exchange;
        protected string Exchange
        {
            get { return exchange;}
        }        
        // Exchange type
        private string exchange_type;
        // Binding key
        private string binding_key;
        protected string Binding_Key
        {
            get { return binding_key;}
        }        
        // Queue
        private QueueDeclareOk queue;
        
        public MessagesHandle(MessageOptions messageOptions)
        {
            try
            {
                // Validate
                messageOptions.validate();

                this.factory = new ConnectionFactory()
                {
                    UserName = messageOptions.Username, 
                    Password = messageOptions.Password, 
                    VirtualHost = messageOptions.VirtualHost, 
                    Protocol = Protocols.AMQP_0_9_1,
                    HostName = messageOptions.HostName,
                    Port = messageOptions.Port
                    // Port = AmqpTcpEndpoint.UseDefaultPort
                };
                this.connection = factory.CreateConnection();
                this.channel = connection.CreateModel();
                this.queue_name = messageOptions.QueueName;
                this.exchange = messageOptions.Exchange;
                this.exchange_type = messageOptions.ExchangeType;
                this.binding_key = messageOptions.BindingKey;                
            }
            catch (RequiredMessageOptionException)
            {
                throw;
            }
            catch (BrokerUnreachableException)
            {
                StringBuilder ex_message = new StringBuilder();
                ex_message.Append("MessageOption - Please verify the following are correct:");
                ex_message.Append(Environment.NewLine);
                ex_message.Append("Hostname, Username, Password, VirtualHost, Port");

                throw new RequiredMessageOptionException(ex_message.ToString());

            }
            catch (Exception)
            {
                throw;
            }                        
        }

        protected void create_exchange()
        {
            // Declare Exchange
            this.channel.ExchangeDeclare(exchange: exchange, type: exchange_type);
        }

        protected void create_queue()
        {
            // Declare Queue
            queue = channel.QueueDeclare(queue: queue_name, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }
        
        protected void bind_queue()
        {
            // Bind Queue to Exchange
            channel.QueueBind(queue: queue, exchange: exchange, routingKey: binding_key);              
        }        
    }
}
