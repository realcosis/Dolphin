using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Inventory.Purse
{
    public class HabboActivityPointNotificationMessageComposer(int balance, int notify, int type) : OutgoingHandler(ServerPacketCode.HabboActivityPointNotificationMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(balance);
            Packet?.WriteInteger(notify);
            Packet?.WriteInteger(type);
        }
    }
}