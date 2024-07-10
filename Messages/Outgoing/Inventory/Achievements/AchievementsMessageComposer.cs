using Dolphin.HabboHotel.Achievements.Models;
using Dolphin.HabboHotel.Users.Models;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Inventory.Achievements
{
    public class AchievementsMessageComposer(Habbo habbo, List<Achievement> achievements) : OutgoingHandler(ServerPacketCode.AchievementsMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(achievements.Count);
            foreach (var achievement in achievements)
            {
                var userData = habbo.Achievements.FirstOrDefault(a => a.AchievementGroup == achievement.GroupName);
                var targetLevel = userData != default ? userData.Level + 1 : 1;
                var totalLevels = achievement.Levels.Count;
                targetLevel = targetLevel > totalLevels ? totalLevels : targetLevel;
                var targetLevelData = achievement.Levels[targetLevel];
                Packet?.WriteInteger(achievement.Id);
                Packet?.WriteInteger(targetLevel);
                Packet?.WriteString(achievement.GroupName + targetLevel);
                Packet?.WriteInteger(0);
                Packet?.WriteInteger(targetLevelData.Requirement);
                Packet?.WriteInteger(targetLevelData.RewardPixels);
                Packet?.WriteInteger(0);
                Packet?.WriteInteger(userData?.Progress ?? 0);
                Packet?.WriteBoolean(userData != default && userData.Level >= totalLevels);
                Packet?.WriteString(achievement.Category!);
                Packet?.WriteString(string.Empty);
                Packet?.WriteInteger(totalLevels);
                Packet?.WriteInteger(userData != default ? 1 : 0);
            }
            Packet?.WriteString(string.Empty);
        }
    }
}