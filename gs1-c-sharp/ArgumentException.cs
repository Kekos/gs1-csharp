using System;

namespace Kekos.Gs1
{
    public class ArgumentException : Exception
    {
        public ArgumentException()
        {
        }

        public ArgumentException(string message)
            : base(message)
        {
        }

        public ArgumentException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
