using Dolphin.Messages.Handler;
using Dolphin.Messages.Outgoing.Messenger;
using Dolphin.Networking.Client;
using RC.Common.Injection;

namespace Dolphin.Messages.Incoming.Messenger
{
    [Singleton]
    class MessengerInitMessageEvent : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
        {
            if (client.Habbo == default)
                return;

            var friends = client.Habbo.Friends.Where(b => b.IsOnline).ToList();

            await client.Send(new MessengerInitMessageComposer());

            if (friends.Count == 0)
                await client.Send(new BuddyListMessageComposer(friends, 1, 0));
            else
            {
                var page = 0;
                var pages = (friends.Count - 1) / 500 + 1;
                foreach (var batch in friends.Chunk(500))
                {
                    await client.Send(new BuddyListMessageComposer([.. batch], pages, page));
                    page++;
                }
            }
        }
    }
}