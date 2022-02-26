using System;
using System.Runtime.Serialization;

namespace Logickflow.Core.Exceptions
{
    /// <summary>
    /// This exception is thrown when there is an abnormal state in the approval operation
    /// </summary>
    [Serializable]
    public class IllegalStateException : ApplicationException
    {
        public IllegalStateException()
        {

        }

        public IllegalStateException(string message)
            : base(message)
        {

        }

        public IllegalStateException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {

        }

        protected IllegalStateException(SerializationInfo
            info, StreamingContext context)
            : base(info, context)
        {
        }

        public IllegalStateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}