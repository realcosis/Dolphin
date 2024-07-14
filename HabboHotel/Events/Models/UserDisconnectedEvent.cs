using Dolphin.HabboHotel.Users.Models;

namespace Dolphin.HabboHotel.Events.Models
{
    public class UserDisconnectedEvent
    {
        public int UserId { get; set; }

        public Habbo? Habbo { get; set; }
    }
}