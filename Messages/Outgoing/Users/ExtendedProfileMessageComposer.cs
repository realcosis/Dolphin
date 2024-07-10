using Dolphin.HabboHotel.Users.Models;
using Dolphin.Messages.Handler;
using Dolphin.Util;
using System;

namespace Dolphin.Messages.Outgoing.Users
{
    public class ExtendedProfileMessageComposer(Habbo habbo, Habbo viewer) : OutgoingHandler(ServerPacketCode.ExtendedProfileMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(habbo.User!.Id);
            Packet?.WriteString(habbo.User!.Username!);
            Packet?.WriteString(habbo.User!.Look!);
            Packet?.WriteString(habbo.User!.Motto!);
            Packet?.WriteString(habbo.User!.LastOnline!.Value.ToString("dd/MM/yyyy"));
            Packet?.WriteInteger(habbo.User!.AchievementScore);
            Packet?.WriteInteger(habbo.Friends.Count);
            Packet?.WriteBoolean(viewer.Friends.Any(f => f.UserId == habbo.User!.Id));
            Packet?.WriteBoolean(viewer.Requests.Any(f => f.ToUser == habbo.User!.Id));
            Packet?.WriteBoolean(habbo.User!.Online);
            Packet?.WriteInteger(habbo.Groups.Count);
            foreach (var group in habbo.Groups)
            {
                Packet?.WriteInteger(group.Id);
                Packet?.WriteString(group.Name!);
                Packet?.WriteString(group.Image!);
                Packet?.WriteString(group.HtmlColorPrimary!);
                Packet?.WriteString(group.HtmlColorSecondary!);
                Packet?.WriteBoolean(false);
                Packet?.WriteInteger(group.OwnerId);
                Packet?.WriteBoolean(group.OwnerId == habbo.User!.Id);
            }
            Packet?.WriteInteger((int)habbo.User!.LastOnline.GetTimeStamp());
            Packet?.WriteBoolean(true);
        }
    }
}