namespace Dolphin.HabboHotel.Users.Models
{
    public class MessengerRequest
    {
        public int ToUser { get; set; }

        public int FromUser { get; set; }

        public string? Username { get; set; }

        public string? Look { get; set; }
    }
}