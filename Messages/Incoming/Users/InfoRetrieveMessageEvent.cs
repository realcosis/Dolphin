using Dolphin.Messages.Handler;
using Dolphin.Messages.Outgoing.Users;
using Dolphin.Networking.Client;
using Dolphin.Injection;

namespace Dolphin.Messages.Incoming.Users
{
    [Singleton]
    class InfoRetrieveMessageEvent : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
        {
            await client.Send(new UserObjectMessageComposer(client.Habbo));
        }
    }
}
