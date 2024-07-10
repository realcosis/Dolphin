using Dolphin.HabboHotel.Achievements;
using Dolphin.HabboHotel.Users;
using Dolphin.Messages.Handler;
using Dolphin.Messages.Outgoing.Handshake;
using Dolphin.Messages.Outgoing.Inventory.Achievements;
using Dolphin.Messages.Outgoing.Inventory.AvatarEffects;
using Dolphin.Messages.Outgoing.Messenger;
using Dolphin.Messages.Outgoing.Navigator;
using Dolphin.Messages.Outgoing.Sounds;
using Dolphin.Networking.Client;
using Dolphin.Injection;

namespace Dolphin.Messages.Incoming.Handshake
{
    [Singleton]
    class SSOTicketMessageEvent(IUsersManager usersManager, IAchievementsManager achievementsManager) : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
        {
            var sessionTicket = packet.ReadString();

            if (string.IsNullOrWhiteSpace(sessionTicket))
            {
                await client.Dispose();
                return;
            }

            var habbo = await usersManager.LoadHabbo(sessionTicket);

            if (habbo == default)
                await client.Dispose();
            else
            {
                client.Habbo = habbo;
                habbo.ClientSession = client;

                await client.Send(new AuthenticationOkMessageComposer());
                await client.Send(new AvatarEffectsMessageComposer(client.Habbo.Effects));
                await client.Send(new NavigatorSettingsMessageComposer(client.Habbo.User!.HomeRoom));
                await client.Send(new FavouritesMessageComposer(client.Habbo.FavoriteRooms));
                await client.Send(new UserRightsMessageComposer(client.Habbo.User.Rank));
                await client.Send(new SoundSettingsMessageComposer(client.Habbo.User.Volume, client.Habbo.User.ChatPreference, client.Habbo.User.FocusPreference));
                await client.Send(new AvailabilityStatusMessageComposer());
                await client.Send(new FriendListUpdateMessageComposer(default, default, 1, client.Habbo.Friends));
                await client.Send(new BadgeDefinitionsMessageComposer([.. achievementsManager.Achievements.Values]));
            }
        }
    }
}