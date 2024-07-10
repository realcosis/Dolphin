using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Inventory.Purse
{
    public class ActivityPointsMessageComposer(int duckets, int crystals) : OutgoingHandler(ServerPacketCode.ActivityPointsMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(2);
            Packet?.WriteInteger(0);
            Packet?.WriteInteger(duckets);
            Packet?.WriteInteger(5); //Diamonds
            Packet?.WriteInteger(crystals);
        }
    }
}