using Dolphin.HabboHotel.Groups.Models;
using Dolphin.HabboHotel.Users.Models;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Groups
{
    public class GroupInformationMessageComposer(Group group, Habbo habbo, bool newWindow, string? ownerName) : OutgoingHandler(ServerPacketCode.GroupInformationMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(group.Id);
            Packet?.WriteBoolean(true);
            Packet?.WriteInteger(group.Type);
            Packet?.WriteString(group.Name!);
            Packet?.WriteString(group.Description!);
            Packet?.WriteString(group.Image!);
            Packet?.WriteInteger(group.RoomId);
            Packet?.WriteString("No room found..");
            Packet?.WriteInteger(group.OwnerId == habbo.User!.Id ? 3 : group.HasRequest(habbo.User!.Id) ? 2 : group.IsMember(habbo.User!.Id) ? 1 : 0);
            Packet?.WriteInteger(group.MemberCount);
            Packet?.WriteBoolean(false);
            Packet?.WriteString(group.DateCreate!);
            Packet?.WriteBoolean(group.OwnerId == habbo.User!.Id);
            Packet?.WriteBoolean(group.IsAdmin(habbo.User!.Id));
            Packet?.WriteString(ownerName ?? "No user found..");
            Packet?.WriteBoolean(newWindow);
            Packet?.WriteBoolean(false);
            Packet?.WriteInteger(group.OwnerId == habbo.User!.Id ? group.RequestCount :
            group.IsAdmin(habbo.User!.Id) ? group.RequestCount :
                group.IsMember(habbo.User!.Id) ? 0 : 0);
            Packet?.WriteBoolean(false);
        }
    }
}