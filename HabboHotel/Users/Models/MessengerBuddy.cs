using Dolphin.DAL.Enums;
using Dolphin.Util;

namespace Dolphin.HabboHotel.Users.Models
{
    public class MessengerBuddy
    {
        public int UserId { get; set; }

        public string? Username { get; set; }

        public string? Look { get; set; }

        public string? Motto { get; set; }

        public RelationshipStatus? Relationship { get; set; }

        public string? Online { get; set; }

        public bool IsOnline => Online.EnumToBool();
    }
}