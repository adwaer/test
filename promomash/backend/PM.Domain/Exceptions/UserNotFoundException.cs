using System;
using System.Runtime.Serialization;

namespace PM.Domain.Exceptions
{
    public class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException() { }

        public UserNotFoundException(string message) : base(message) { }

        public UserNotFoundException(string message, Exception inner) : base(message, inner) { }

        protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}