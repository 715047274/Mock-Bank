using System;

namespace MockBank.Application.Common.Exceptions
{
    public class BKErrorException: Exception
    {
        
        public BKErrorException() : base()
        {
        }
        
        public BKErrorException(string code, string message)
            : base(message)
        {
        }

        public BKErrorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public BKErrorException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}