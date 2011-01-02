namespace EmmLabs.Remote.Core
{
    public interface ICommunicationChannel
    {
        string Read();
        void Write(IMessage message);
    }
}
