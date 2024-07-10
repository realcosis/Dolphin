using Dolphin.HabboHotel.Catalogs;
using Dolphin.Messages.Handler;
using Dolphin.Networking.Client;
using Dolphin.Injection;
using Dolphin.Messages.Outgoing.Catalogs;

namespace Dolphin.Messages.Incoming.Catalogs
{
    [Singleton]
    public class GetCatalogIndexMessageEvent(ICatalogsManager catalogsManager) : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
            => await client.Send(new CatalogIndexMessageComposer(client, [.. catalogsManager.Pages.Values]));
    }
}