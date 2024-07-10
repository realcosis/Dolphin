namespace Dolphin.Networking
{
    public interface IServer
    {
        Task Start();

        Task Stop();
    }
}