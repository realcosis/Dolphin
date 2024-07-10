namespace Dolphin.HabboHotel.Groups.Models
{
    public class GroupMember
    {
        public int Rank { get; set; }

        public DateTime EnteredAt { get; set; }

        public int UserId { get; set; }

        public string? Username { get; set; }

        public string? Look { get; set; }
    }
}