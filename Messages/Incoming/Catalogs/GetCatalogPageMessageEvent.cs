using Dolphin.HabboHotel.Catalogs;
using Dolphin.Messages.Handler;
using Dolphin.Messages.Outgoing.Catalogs;
using Dolphin.Networking.Client;
using RC.Common.Injection;

namespace Dolphin.Messages.Incoming.Catalogs
{
    [Singleton]
    public class GetCatalogPageMessageEvent(ICatalogsManager catalogsManager) : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
        {
            var pageId = packet.ReadInt();
            packet.ReadInt();
            var cataMode = packet.ReadString();

            var page = catalogsManager.Pages.Values.FirstOrDefault(p => p.Id == pageId);
            if (page == default)
                return;

            if (!page.Visible)
                return;

            await client.Send(new CatalogPageMessageComposer(page, cataMode));
        }
    }
}