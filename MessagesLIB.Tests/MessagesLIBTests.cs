using NUnit.Framework;
using MessagesLIB;
using System.Collections;

namespace Tests
{
     [TestFixture]
    public class MessagesLIBTests
    {
        private static MessageOptions _message_options;
        private MessageSender _sender;
        private MessageReceiver _receiver;

        /// <summary>
        /// Method: BrokerConnectionFail()
        /// Description:
        ///     Test all expected exceptions are thrown
        ///     Makes use of <see cref="MessageOptionsFactoryClass"/> for test cases
        /// Returns: void
        /// </summary>
        /// <param name="messageOptions">An instance of MessageOptions</param>
        [Test, TestCaseSource(typeof(MessageOptionsFactoryClass), "TestCases")]
        public void BrokerConnectionFail(MessageOptions messageOptions)
        {
            MessageSender sender;
            var ex = Assert.Throws<RequiredMessageOptionException>
            (
                () => sender = new MessageSender(messageOptions)
            );
            Assert.That(ex, Is.TypeOf(typeof(RequiredMessageOptionException)));
        }

        /// <summary>
        /// Method: SendMessage()
        /// Description:
        ///     Test sending message, _send.send() must not throw an exception
        /// Returns: void
        /// </summary>
        /// <param>String</param>
        [Test]
        public void SendMessage()
        {
            _message_options = new MessageOptions();  
            _sender = new MessageSender(_message_options);
            Assert.DoesNotThrow(() => _sender.send("test message from NUnit [Test]"));
        }
        /// <summary>
        /// Method: ReceiveMessage()
        /// Description:
        ///     Test receive messages, _receiver.Receive() must not throw an exception
        /// Returns: void
        /// </summary>
        /// <param>None</param>
        [Test]
        public void ReceiveMessage()
        {
            _message_options = new MessageOptions();  
            _receiver = new MessageReceiver(_message_options);    
            Assert.DoesNotThrow(() => _receiver.Receive());
        }        
    }

    public class MessageOptionsFactoryClass
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(new MessageOptions(){Username="Invalid_Username"});
                yield return new TestCaseData(new MessageOptions(){Password="Invalid_Password"});
                yield return new TestCaseData(new MessageOptions(){VirtualHost="Invalid_VirtualHost"});
                yield return new TestCaseData(new MessageOptions(){Port=0});
                yield return new TestCaseData(new MessageOptions(){Username=""});
                yield return new TestCaseData(new MessageOptions(){Password=""});
                yield return new TestCaseData(new MessageOptions(){VirtualHost=""});
                yield return new TestCaseData(new MessageOptions(){HostName=""});
                yield return new TestCaseData(new MessageOptions(){QueueName=""});
                yield return new TestCaseData(new MessageOptions(){Exchange=""});
                yield return new TestCaseData(new MessageOptions(){ExchangeType=""});
                yield return new TestCaseData(new MessageOptions(){BindingKey=""});                
            }
        }
    }  
}