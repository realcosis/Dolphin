using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Inventory.Achievements
{
    public class AchievementScoreMessageComposer(int achievementScore) : OutgoingHandler(ServerPacketCode.AchievementScoreMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(achievementScore);
        }
    }
}