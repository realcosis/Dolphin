using Dolphin.HabboHotel.Achievements.Models;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Inventory.Achievements
{
    public class BadgeDefinitionsMessageComposer(List<Achievement> achievements) : OutgoingHandler(ServerPacketCode.BadgeDefinitionsMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(achievements.Count);
            foreach (var achievement in achievements)
            {
                Packet?.WriteString(achievement.GroupName!.Replace("ACH_", ""));
                Packet?.WriteInteger(achievement.Levels.Count);
                foreach (var level in achievement.Levels.Values)
                {
                    Packet?.WriteInteger(level.Level);
                    Packet?.WriteInteger(level.Requirement);
                }
            }
        }
    }
}