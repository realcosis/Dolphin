using Dolphin.Messages.Incoming;
using Dolphin.Networking.Client;

namespace Dolphin.Messages.Handler
{
    public interface IIncomingHandler
    {
        Task Handle(ClientSession client, IncomingPacket packet);
    }
}