using Dolphin.Messages.Handler;
using Dolphin.Messages.Outgoing.Users;
using Dolphin.Networking.Client;
using Dolphin.Injection;

namespace Dolphin.Messages.Incoming.Users
{
    [Singleton]
    public class GetWardrobeMessageEvent : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
        {
            if (client.Habbo == default)
                return;

            await client.Send(new WardrobeMessageComposer(client.Habbo.Wardrobes));
        }
    }
}