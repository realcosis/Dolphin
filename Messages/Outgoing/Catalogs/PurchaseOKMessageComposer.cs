using Dolphin.HabboHotel.Catalogs.Models;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Catalogs
{
    public class PurchaseOKMessageComposer(CatalogItem item) : OutgoingHandler(ServerPacketCode.PurchaseOKMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(item.ItemBase!.Id);
            Packet?.WriteString(item.ItemBase!.ItemName!);
            Packet?.WriteBoolean(false);
            Packet?.WriteInteger(item.CostCredits);
            Packet?.WriteInteger(item.CostPoints);
            Packet?.WriteInteger(0);
            Packet?.WriteBoolean(true);
            Packet?.WriteInteger(1);
            Packet?.WriteString(item.ItemBase!.Type!.ToString().ToLower());
            Packet?.WriteInteger(item.ItemBase!.SpriteId);
            Packet?.WriteString("");
            Packet?.WriteInteger(1);
            Packet?.WriteInteger(0);
            Packet?.WriteString("");
            Packet?.WriteInteger(1);
        }
    }
}