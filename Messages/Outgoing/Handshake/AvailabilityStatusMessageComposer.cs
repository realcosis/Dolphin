using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Handshake
{
    public class AvailabilityStatusMessageComposer() : OutgoingHandler(ServerPacketCode.AvailabilityStatusMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteBoolean(true);
            Packet?.WriteBoolean(false);
            Packet?.WriteBoolean(true);
        }
    }
}