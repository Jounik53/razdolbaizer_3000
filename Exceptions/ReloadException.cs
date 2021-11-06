using System;
using System.Collections.Generic;
using System.Text;

namespace razdolbaizer_3000.Exceptions
{
    public class ReloadException : Exception
    {
        public ReloadException(string message) : base(message)
        {
        }
    }
}
