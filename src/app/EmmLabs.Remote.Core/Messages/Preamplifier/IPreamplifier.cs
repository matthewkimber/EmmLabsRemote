namespace EmmLabs.Remote.Core
{
    public interface IPreamplifier : IProduct
    {
        bool IsMuted { get; }
        int Volume { get; set; }
    }
}