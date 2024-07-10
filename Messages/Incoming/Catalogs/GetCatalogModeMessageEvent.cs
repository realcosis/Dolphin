using Dolphin.HabboHotel.Catalogs;
using Dolphin.Messages.Handler;
using Dolphin.Networking.Client;
using Dolphin.Messages.Outgoing.Catalogs;
using Dolphin.Injection;

namespace Dolphin.Messages.Incoming.Catalogs
{
    [Singleton]
    public class GetCatalogModeMessageEvent(ICatalogsManager catalogsManager) : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
            => await client.Send(new CatalogIndexMessageComposer(client, [.. catalogsManager.Pages.Values]));
    }
}