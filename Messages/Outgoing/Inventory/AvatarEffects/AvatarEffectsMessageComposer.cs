using Dolphin.HabboHotel.Users.Models;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Inventory.AvatarEffects
{
    public class AvatarEffectsMessageComposer(List<UserEffect> userEffects) : OutgoingHandler(ServerPacketCode.AvatarEffectsMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(userEffects.Count);
            foreach (var effect in userEffects)
            {
                Packet?.WriteInteger(effect.EffectId);
                Packet?.WriteInteger(0);
                Packet?.WriteInteger(effect.TotalDuration);
                Packet?.WriteInteger(effect.IsActivated ? effect.Quantity - 1 : effect.Quantity);
                Packet?.WriteInteger(effect.IsActivated ? effect.TimeLeft : -1);
                Packet?.WriteBoolean(false);
            }
        }
    }
}