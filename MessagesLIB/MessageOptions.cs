using System;

namespace MessagesLIB
{
    /// <summary>
    /// Defines Broker connection, Queue name, Exchange, Exchange type, Binding key
    
    /// Default Constructor inits all properties with default values
    /// Defaults:
    ///          Username = "guest";
    ///          Password = "guest";
    ///          VirtualHost = "/";
    ///          HostName = "localhost";  
    ///          Port = 5672;    
    ///          QueueName = "greeting_msg.queue";     
    ///          Exchange = "greeting_msg.exchange";  
    ///          ExchangeType = "direct"; 
    ///          BindingKey = "greeting_msg.key";
    
    /// Overload Constructor NOT provided
    /// Must init all properties to use custom options for broker

    /// Exceptions: RequiredMessageOptionException
    ///     thrown when any of the properties is null or empty
    /// </summary>
    public class MessageOptions
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public string QueueName { get; set; }
        public string Exchange { get; set; }
        public string ExchangeType { get; set; }
        public string BindingKey { get; set; }

        public MessageOptions()
        {
            this.Username = "guest";
            this.Password = "guest";
            this.VirtualHost = "/";
            this.HostName = "localhost";  
            this.Port = 5672;    
            this.QueueName = "greeting_msg.queue";     
            this.Exchange = "greeting_msg.exchange";  
            this.ExchangeType = "direct"; 
            this.BindingKey = "greeting_msg.key";
        }

        public void validate()
        {
            var my_properties = this.GetType().GetProperties();
            
            foreach (var my_property in my_properties)
            {
                if (String.IsNullOrEmpty(my_property.GetValue(this).ToString().Trim()))
                {
                    throw new RequiredMessageOptionException(String.Format("MessageOption - {0} is required", my_property.Name));
                }
            }
        }
    }    
}