using Dolphin.HabboHotel.Groups.Models;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Groups
{
    public class GroupBadgePartsMessageComposer(List<GroupElement> groupElements) : OutgoingHandler(ServerPacketCode.GroupBadgePartsMessageComposer)
    {
        public override void Compose()
        {
            var bases = groupElements.Where(ge => ge.Type!.Equals("base", StringComparison.CurrentCultureIgnoreCase)).ToList();
            Packet?.WriteInteger(bases.Count);
            foreach (var @base in bases)
            {
                Packet?.WriteInteger(@base.Id);
                Packet?.WriteString(@base.ExtraData1!);
                Packet?.WriteString(@base.ExtraData2!);
            }

            var symbols = groupElements.Where(ge => ge.Type!.Equals("symbol", StringComparison.CurrentCultureIgnoreCase)).ToList();
            Packet?.WriteInteger(symbols.Count);
            foreach (var symbol in symbols)
            {
                Packet?.WriteInteger(symbol.Id);
                Packet?.WriteString(symbol.ExtraData1!);
                Packet?.WriteString(symbol.ExtraData2!);
            }

            var baseColors = groupElements.Where(ge => ge.Type!.Equals("color1", StringComparison.CurrentCultureIgnoreCase)).ToList();
            Packet?.WriteInteger(baseColors.Count);
            foreach (var baseColor in baseColors)
            {
                Packet?.WriteInteger(baseColor.Id);
                Packet?.WriteString(baseColor.ExtraData1!);
            }

            var symbolColors = groupElements.Where(ge => ge.Type!.Equals("color2", StringComparison.CurrentCultureIgnoreCase)).ToList();
            Packet?.WriteInteger(symbolColors.Count);
            foreach (var symbolColor in symbolColors)
            {
                Packet?.WriteInteger(symbolColor.Id);
                Packet?.WriteString(symbolColor.ExtraData1!);
            }

            var backgroundColors = groupElements.Where(ge => ge.Type!.Equals("color3", StringComparison.CurrentCultureIgnoreCase)).ToList();
            Packet?.WriteInteger(backgroundColors.Count);
            foreach (var backgroundColor in backgroundColors)
            {
                Packet?.WriteInteger(backgroundColor.Id);
                Packet?.WriteString(backgroundColor.ExtraData1!);
            }
        }
    }
}