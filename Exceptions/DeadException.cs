using System;
using System.Collections.Generic;
using System.Text;

namespace razdolbaizer_3000.Exceptions
{
    public class DeadException : Exception
    {
        public DeadException(string message) : base(message)
        {
        }
    }
}
