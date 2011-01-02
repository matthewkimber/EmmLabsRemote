using System;

namespace EmmLabs.Remote.Core
{
    public class InvalidFrameFormatException : Exception
    {
        public string Frame { get; private set; }

        public InvalidFrameFormatException(string frame)
            : this(frame, "The message is in an invalid format.")
        {}

        public InvalidFrameFormatException(string frame, string message)
            : base(message)
        {
            Frame = frame;
        }
    }
}
