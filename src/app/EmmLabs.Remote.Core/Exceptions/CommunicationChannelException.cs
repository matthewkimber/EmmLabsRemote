using System;

namespace EmmLabs.Remote.Core
{
    internal class CommunicationChannelException : Exception
    {
        public CommunicationChannelException(string message, Exception innerException)
            : base(message, innerException)
        {}
    }
}