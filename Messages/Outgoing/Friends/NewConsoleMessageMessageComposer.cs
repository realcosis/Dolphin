using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Friends
{
    public class NewConsoleMessageMessageComposer(int userId, string message) : OutgoingHandler(ServerPacketCode.NewConsoleMessageMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(userId);
            Packet?.WriteString(message);
            Packet?.WriteInteger(0);
        }
    }
}