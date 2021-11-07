using System;

namespace razdolbaizer_3000.Exceptions
{
    public class DeadException : Exception
    {
        public DeadException(string message) : base(message)
        {
        }
    }
}
