namespace Dolphin
{
    public interface IEmulator
    {
        Task Start();

        Task Dispose();
    }
}