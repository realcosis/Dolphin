using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Handshake
{
    public class UserRightsMessageComposer(int rank) : OutgoingHandler(ServerPacketCode.UserRightsMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(2); //Club level
            Packet?.WriteInteger(rank);
            Packet?.WriteBoolean(false); //Is an ambassador
        }
    }
}