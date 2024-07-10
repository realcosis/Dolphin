using Dolphin.HabboHotel.Users;
using Dolphin.HabboHotel.Users.Models;
using Dolphin.Messages.Handler;
using Dolphin.Networking.Client;
using Dolphin.Injection;

namespace Dolphin.Messages.Incoming.Users
{
    [Singleton]
    class SaveWardrobeOutfitMessageEvent(IUsersManager usersManager) : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
        {
            if (client.Habbo == default)
                return;

            var slotId = packet.ReadInt();
            var look = packet.ReadString();
            var gender = packet.ReadString();

            var wardrobe = client.Habbo.Wardrobes.FirstOrDefault(w => w.SlotId == slotId);
            var isNew = default(bool);
            if (wardrobe == default)
            {
                wardrobe = new UserWardrobe
                {
                    SlotId = slotId,
                    Gender = gender,
                    Look = look
                };
                isNew = true;
            }
            else
            {
                wardrobe.Look = look;
                wardrobe.Gender = gender;
                isNew = false;
            }

            await usersManager.UpsertSlot(client.Habbo.User!.Id, wardrobe, isNew);
        }
    }
}