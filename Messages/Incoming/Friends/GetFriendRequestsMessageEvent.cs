using Dolphin.Messages.Handler;
using Dolphin.Messages.Outgoing.Friends;
using Dolphin.Networking.Client;
using RC.Common.Injection;

namespace Dolphin.Messages.Incoming.Friends
{
    [Singleton]
    class GetFriendRequestsMessageEvent : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
        {
            if (client.Habbo == default || client.Habbo?.User == default)
                return;

            await client.Send(new FriendRequestsMessageComposer(client.Habbo!.Requests));
        }
    }
}