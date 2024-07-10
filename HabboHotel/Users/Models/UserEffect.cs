using Dolphin.Util;

namespace Dolphin.HabboHotel.Users.Models
{
    public class UserEffect
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int EffectId { get; set; } = 1;

        public int TotalDuration { get; set; }

        public bool IsActivated { get; set; }

        public double ActivatedStamp { get; set; }

        public int Quantity { get; set; }

        public double TimeUsed => TextHandling.GetNow() - ActivatedStamp;

        public int TimeLeft
        {
            get
            {
                var tl = IsActivated ? ActivatedStamp - TimeUsed : TotalDuration;
                if (tl < 0) tl = 0;
                return (int)tl;
            }
        }

        public bool HasExpired => IsActivated && TimeLeft <= 0;
    }
}