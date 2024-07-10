using Dolphin.Messages.Handler;
using Dolphin.Messages.Outgoing.Inventory.Purse;
using Dolphin.Networking.Client;
using RC.Common.Injection;

namespace Dolphin.Messages.Incoming.Inventory.Purse
{
    [Singleton]
    class GetCreditsInfoMessageEvent : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
        {
            if (client.Habbo == default || client.Habbo?.User == default)
                return;

            await client.Send(new CreditBalanceMessageComposer(client.Habbo.User.Credits));
            await client.Send(new ActivityPointsMessageComposer(client.Habbo.User.Duckets, client.Habbo.User.Crystals));
        }
    }
}