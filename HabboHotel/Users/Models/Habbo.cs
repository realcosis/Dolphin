using Dolphin.HabboHotel.Groups.Models;
using Dolphin.HabboHotel.Rooms.Models;
using Dolphin.Networking.Client;

namespace Dolphin.HabboHotel.Users.Models
{
    public class Habbo
    {
        public HabboData? User { get; set; }

        public ClientSession? ClientSession { get; set; }

        public List<UserBadge> Badges { get; set; } = [];

        public List<UserAchievement> Achievements { get; set; } = [];

        public List<UserWardrobe> Wardrobes { get; set; } = [];

        public List<UserItem> Items { get; set; } = [];

        public bool IsVip { get; set; }

        public List<MessengerBuddy> Friends { get; set; } = [];

        public List<MessengerRequest> Requests { get; set; } = [];

        public List<Room> Rooms { get; set; } = [];

        public List<Room> FavoriteRooms { get; set; } = [];

        public List<UserEffect> Effects { get; set; } = [];

        public List<Group> Groups { get; set; } = [];

        public List<IgnoredUser> IgnoredUsers { get; set; } = [];
    }
}