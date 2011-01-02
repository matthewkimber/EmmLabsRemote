using System;

namespace EmmLabs.Remote.Core
{
    public class InvalidVolumeLevelException : Exception
    {
        public int InvalidLevel { get; private set; }

        public InvalidVolumeLevelException(int invalidLevel)
            : this(invalidLevel, "Please enter a volume between 1 and 100.")
        {}

        public InvalidVolumeLevelException(int invalidLevel, string message)
            : base(message)
        {
            InvalidLevel = invalidLevel;
        }
    }
}
