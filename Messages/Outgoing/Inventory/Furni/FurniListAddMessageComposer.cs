using Dolphin.HabboHotel.Users.Models;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Inventory.Furni
{
    public class FurniListAddMessageComposer(UserItem item) : OutgoingHandler(ServerPacketCode.FurniListAddMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(item.ItemId);
            Packet?.WriteString(item.ItemBase!.Type!.ToString().ToUpper());
            Packet?.WriteInteger(item.ItemId);
            Packet?.WriteInteger(item.ItemBase!.SpriteId);

            if (item.LimitedNo > 0)
            {
                Packet?.WriteInteger(1);
                Packet?.WriteInteger(256);
                Packet?.WriteString(item.ExtraData!);
                Packet?.WriteInteger(item.LimitedNo);
                Packet?.WriteInteger(item.LimitedTot);
            }
            Packet?.WriteInteger(1);
            Packet?.WriteInteger(0);
            Packet?.WriteString(item.ExtraData ?? string.Empty);

            Packet?.WriteBoolean(item.ItemBase!.AllowRecycle);
            Packet?.WriteBoolean(item.ItemBase!.AllowTrade);
            Packet?.WriteBoolean(item.LimitedNo == 0 && item.ItemBase!.AllowInventoryStack);
            Packet?.WriteBoolean(false); //todo is rare
            Packet?.WriteInteger(-1);
            Packet?.WriteBoolean(true);
            Packet?.WriteInteger(-1);

            if (!item.ItemBase!.Type!.ToString().Equals("I", StringComparison.CurrentCultureIgnoreCase))
            {
                Packet?.WriteString(string.Empty);
                Packet?.WriteInteger(0);
            }
        }
    }
}