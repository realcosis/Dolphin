using Dolphin.Messages.Handler;
using Dolphin.Messages.Incoming;
using Dolphin.Networking.Client;
using System.Collections.Concurrent;

namespace Dolphin.Messages
{
    public interface IMessageManager
    {
        ConcurrentDictionary<short, IIncomingHandler> Incomings { get; }

        Task Execute(ClientSession client, IncomingPacket packet);
    }
}