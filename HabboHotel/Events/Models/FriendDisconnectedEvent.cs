using Dolphin.HabboHotel.Users.Models;

namespace Dolphin.HabboHotel.Events.Models
{
    public class FriendDisconnectedEvent : UserDisconnectedEvent
    {
        public List<MessengerBuddy> OnlineFriends { get; set; } = [];
    }
}