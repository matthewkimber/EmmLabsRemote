using System;

namespace EmmLabs.Remote.Core
{
    public class InvalidInputException : Exception
    {
        public int InvalidInput { get; private set; }

        public InvalidInputException(int invalidInput)
            : this(invalidInput, "Please select a valid input number.")
        {}

        public InvalidInputException(int invalidInput, string message)
            : base(message)
        {
            InvalidInput = invalidInput;
        }
    }
}
