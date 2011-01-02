namespace EmmLabs.Remote.Core
{
    public interface IProduct
    {
        string Class { get; }
        void SendCommand(IMessage message);
    }
}
