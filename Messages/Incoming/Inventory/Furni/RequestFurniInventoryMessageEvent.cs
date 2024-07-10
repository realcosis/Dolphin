using Dolphin.HabboHotel.Items.Models;
using Dolphin.Messages.Handler;
using Dolphin.Messages.Outgoing.Inventory.Furni;
using Dolphin.Networking.Client;
using Dolphin.Injection;

namespace Dolphin.Messages.Incoming.Inventory.Furni
{
    [Scoped]
    class RequestFurniInventoryMessageEvent : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
        {
            if (client.Habbo == default)
                return;

            var items = client.Habbo.Items;
            var page = 0;
            var pages = ((items.Count - 1) / 700) + 1;

            if (items.Count == 0)
                await client.Send(new FurniListMessageComposer([.. items], 1, 0));
            else
            {
                foreach (var batch in items.Chunk(700))
                {
                    await client.Send(new FurniListMessageComposer([.. batch], pages, page));
                    page++;
                }
            }
        }
    }
}