using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Inventory.Purse
{
    public class CreditBalanceMessageComposer(int credits) : OutgoingHandler(ServerPacketCode.CreditBalanceMessageComposer)
    {
        public override void Compose()
            => Packet?.WriteString(credits + ".0");
    }
}