using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Sounds
{
    public class SoundSettingsMessageComposer(List<int> volumes, bool chatPreference, bool focusPreference) : OutgoingHandler(ServerPacketCode.SoundSettingsMessageComposer)
    {
        public override void Compose()
        {
            foreach (int volume in volumes)
            {
                Packet?.WriteInteger(volume);
            }
            Packet?.WriteBoolean(chatPreference);
            Packet?.WriteBoolean(false);
            Packet?.WriteBoolean(focusPreference);
            Packet?.WriteInteger(0);
            Packet?.WriteInteger(0);
            Packet?.WriteInteger(0);
        }
    }
}