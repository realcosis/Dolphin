using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Inventory.Furni
{
    public class FurniListNotificationMessageComposer(int id, int type) : OutgoingHandler(ServerPacketCode.FurniListNotificationMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(1);
            Packet?.WriteInteger(type);
            Packet?.WriteInteger(1);
            Packet?.WriteInteger(id);
        }
    }
}