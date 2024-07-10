using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Messenger
{
    public class MessengerInitMessageComposer() : OutgoingHandler(ServerPacketCode.MessengerInitMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(500);
            Packet?.WriteInteger(300);
            Packet?.WriteInteger(800);
            Packet?.WriteInteger(0);
        }
    }
}