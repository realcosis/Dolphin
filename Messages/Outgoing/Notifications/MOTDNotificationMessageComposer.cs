using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Notifications
{
    public class MOTDNotificationMessageComposer(string message) : OutgoingHandler(ServerPacketCode.MOTDNotificationMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(1);
            Packet?.WriteString(message);
        }
    }
}