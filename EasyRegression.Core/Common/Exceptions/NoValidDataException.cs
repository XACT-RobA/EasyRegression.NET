using System;

namespace EasyRegression.Core.Common.Exceptions
{
    public class NoValidDataException : Exception
    {
        public NoValidDataException()
            : base("No valid data provided") { }

        public NoValidDataException(string message)
            : base(message) { }

        public NoValidDataException(string message, Exception inner)
            : base(message, inner) { }
    }
}