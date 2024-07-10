using Dolphin.HabboHotel.Catalogs.Models;
using Dolphin.Messages.Handler;
using Dolphin.Messages.Outgoing;
using Dolphin.Networking.Client;

namespace Dolphin.Outgoing.Catalogs
{
    public class CatalogIndexMessageComposer(ClientSession session, List<CatalogPage> pages) : OutgoingHandler(ServerPacketCode.CatalogIndexMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteBoolean(true);
            Packet?.WriteInteger(0);
            Packet?.WriteInteger(-1);
            Packet?.WriteString("root");
            Packet?.WriteString(string.Empty);
            Packet?.WriteInteger(0);
            Packet?.WriteInteger(CalculateTreeSize(pages, -1));

            CalculateTreeStructure(pages, -1);

            Packet?.WriteBoolean(false);
            Packet?.WriteString("NORMAL");
        }

        #region private methods

        private void CalculateTreeStructure(List<CatalogPage> pages, int parentId)
        {
            foreach (var page in pages.Where(p => p.ParentId == parentId && p.MinimumRank <= session.Habbo!.User!.Rank).OrderBy(p => p.OrderNumber).ToList())
            {
                Packet?.WriteBoolean(page.Visible);
                Packet?.WriteInteger(page.IconImage);
                Packet?.WriteInteger(page.Id);
                Packet?.WriteString("");
                Packet?.WriteString(page.Caption!);
                Packet?.WriteInteger(0);
                Packet?.WriteInteger(CalculateTreeSize(pages, page.Id));

                CalculateTreeStructure(pages, page.Id);
            }
        }

        private int CalculateTreeSize(List<CatalogPage> pages, int parentId)
            => pages.Where(p => p.ParentId == parentId && p.MinimumRank <= session.Habbo!.User!.Rank).Count();

        #endregion
    }
}