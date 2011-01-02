namespace EmmLabs.Remote.Core
{
    public interface IInput
    {
        int Number { get; }
        string Name { get; set; }
        int Volume { get; set; }
    }
}