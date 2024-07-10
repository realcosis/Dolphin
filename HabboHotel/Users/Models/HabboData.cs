namespace Dolphin.HabboHotel.Users.Models
{
    public class HabboData
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Motto { get; set; }

        public string? Look { get; set; }

        public string? Gender { get; set; }

        public int Rank { get; set; }

        public int AchievementScore { get; set; }

        public bool Online { get; set; }

        public DateTime? LastOnline { get; set; }

        public List<int> Volume { get; set; } = [100, 100, 100, 18];

        public int HomeRoom { get; set; }

        public bool FocusPreference { get; set; }

        public bool ChatPreference { get; set; }

        public int Credits { get; set; }

        public int Duckets { get; set; }

        public int Crystals { get; set; }
    }
}