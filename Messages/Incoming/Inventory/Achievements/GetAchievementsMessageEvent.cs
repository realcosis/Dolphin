using Dolphin.HabboHotel.Achievements;
using Dolphin.Messages.Handler;
using Dolphin.Messages.Outgoing.Inventory.Achievements;
using Dolphin.Networking.Client;
using Dolphin.Injection;

namespace Dolphin.Messages.Incoming.Inventory.Achievements
{
    [Singleton]
    class GetAchievementsMessageEvent(IAchievementsManager achievementsManager) : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
        {
            if (client.Habbo == default || client.Habbo?.User == default)
                return;

            await client.Send(new AchievementScoreMessageComposer(client.Habbo!.User!.AchievementScore));
            await client.Send(new AchievementsMessageComposer(client.Habbo, achievementsManager.Achievements.Values.ToList()));
        }
    }
}