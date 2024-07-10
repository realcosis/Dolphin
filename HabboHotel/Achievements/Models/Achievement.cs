using System.Collections.Concurrent;

namespace Dolphin.HabboHotel.Achievements.Models
{
    public class Achievement
    {
        public int Id { get; set; }

        public string? GroupName { get; set; }

        public string? Category { get; set; }

        public ConcurrentDictionary<int, AchievementLevel> Levels { get; set; } = [];

        public void AddLevel(AchievementLevel Level)
            => Levels.TryAdd(Level.Level, Level);
    }
}