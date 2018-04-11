using System;

namespace MessagesLIB
{
    public class RequiredMessageOptionException: Exception
    {
        public RequiredMessageOptionException() :base(){}

        public RequiredMessageOptionException(string message) :base(message){}
    }    
}