using Dolphin.HabboHotel.Rooms.Models.Navigators;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Navigator
{
    public class UserFlatCatsMessageComposer(List<NavigatorCategory> navigatorCategories) : OutgoingHandler(ServerPacketCode.UserFlatCatsMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(navigatorCategories.Count);
            foreach (var category in navigatorCategories)
            {
                Packet?.WriteInteger(category.Id);
                Packet?.WriteString(category.Caption!);
                Packet?.WriteBoolean(true);
                Packet?.WriteBoolean(false);
                Packet?.WriteString(category.Caption!);
                if (category.Caption!.StartsWith("${"))
                    Packet?.WriteString("");
                else
                    Packet?.WriteString(category.Caption!);
                Packet?.WriteBoolean(false);
            }
        }
    }
}