using System;
using System.Runtime.Serialization;

namespace PM.Domain.Exceptions
{
    [Serializable]
    public class InvalidRequestException : ApplicationException
    {
        public InvalidRequestException() { }

        public InvalidRequestException(string message) : base(message) { }

        public InvalidRequestException(string message, Exception inner) : base(message, inner) { }

        protected InvalidRequestException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}