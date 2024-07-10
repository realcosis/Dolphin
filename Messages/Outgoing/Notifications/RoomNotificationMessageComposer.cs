using Dolphin.Core.Models;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Notifications
{
    public class RoomNotificationMessageComposer(string type, List<KeyValue> keyValues) : OutgoingHandler(ServerPacketCode.RoomNotificationMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteString(type);
            Packet?.WriteInteger(keyValues.Count);
            foreach (var keyValue in keyValues)
            {
                Packet?.WriteString(keyValue.Key!);
                Packet?.WriteString(keyValue.Value!);
            }
        }
    }
}