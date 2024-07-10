namespace Dolphin.HabboHotel.Achievements.Models
{
    public class AchievementLevel
    {
        public int Level { get; set; }

        public int RewardPixels { get; set; }

        public int? RewardPoints { get; set; }

        public int Requirement { get; set; }
    }
}