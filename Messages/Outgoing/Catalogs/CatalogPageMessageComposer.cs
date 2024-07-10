using Dolphin.HabboHotel.Catalogs.Models;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Catalogs
{
    public class CatalogPageMessageComposer(CatalogPage page, string cataMode) : OutgoingHandler(ServerPacketCode.CatalogPageMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(page.Id);
            Packet?.WriteString(cataMode);
            Packet?.WriteString(page.PageLayout!);

            switch (page.PageLayout!)
            {
                case "guild_frontpage":
                case "guild_forum":
                case "guild_custom_furni":
                case "vip_buy":
                    Packet?.WriteInteger(2);
                    Packet?.WriteString(page.PageHeadline!);
                    Packet?.WriteString(page.PageTeaser!);
                    Packet?.WriteInteger(3);
                    Packet?.WriteString(page.PageTextOne ?? string.Empty);
                    Packet?.WriteString(page.PageTextTwo ?? string.Empty);
                    Packet?.WriteString(page.PageTextTeaser ?? string.Empty);
                    Packet?.WriteInteger(0);
                    Packet?.WriteInteger(-1);
                    Packet?.WriteBoolean(false);
                    break;
                case "default_3x3":
                    Packet?.WriteInteger(3);
                    Packet?.WriteString(page.PageHeadline!);
                    Packet?.WriteString(page.PageTeaser!);
                    Packet?.WriteString(page.PageSpecial ?? string.Empty);
                    Packet?.WriteInteger(3);
                    Packet?.WriteString(page.PageTextOne ?? string.Empty);
                    Packet?.WriteString(page.PageTextTwo ?? string.Empty);
                    Packet?.WriteString(page.PageTextTeaser ?? string.Empty);
                    Packet?.WriteInteger(page.Items.Count);
                    foreach (var item in page.Items)
                    {
                        Packet?.WriteInteger(item.Id);
                        Packet?.WriteString(item.CatalogName!);
                        Packet?.WriteBoolean(false);
                        Packet?.WriteInteger(item.CostCredits);
                        Packet?.WriteInteger(item.CostPoints);
                        Packet?.WriteInteger(item.PointsType);
                        Packet?.WriteBoolean(true);
                        Packet?.WriteInteger(1);
                        Packet?.WriteString(item.ItemBase!.Type!);
                        if (item.ItemBase!.Type!.Equals("b", StringComparison.InvariantCultureIgnoreCase))
                            Packet?.WriteString(item.ItemBase!.ItemName!);
                        else
                        {
                            Packet?.WriteInteger(item.ItemBase!.SpriteId);
                            if (item.ItemBase!.ItemName!.Contains("wallpaper_single") || item.ItemBase!.ItemName!.Contains("floor_single") || item.ItemBase!.ItemName!.Contains("landscape_single"))
                                Packet?.WriteString(item.ItemBase!.ItemName!.Split("_")[2]);
                            else if (item.ItemBase!.ItemName!.Contains("bot") && item.ItemBase!.Type!.Equals("R", StringComparison.InvariantCultureIgnoreCase))
                            {
                                bool lookFound = false;
                                foreach (var s in item.Extradata!.Split(";"))
                                {
                                    if (s.StartsWith("figure:"))
                                    {
                                        lookFound = true;
                                        Packet?.WriteString(s.Replace("figure:", ""));
                                        break;
                                    }
                                }

                                if (!lookFound)
                                    Packet?.WriteString(item.Extradata!);
                            }
                            else if (item.ItemBase!.Type!.Equals("R", StringComparison.InvariantCultureIgnoreCase) &&
                                     item.ItemBase!.ItemName!.Equals("poster", StringComparison.InvariantCultureIgnoreCase) &&
                                     item.ItemBase!.ItemName!.StartsWith("SONG "))
                                Packet?.WriteString(item.Extradata!);
                            else
                                Packet?.WriteString("");

                            Packet?.WriteInteger(item.Amount);
                            Packet?.WriteBoolean(item.LimitedStack > 0);
                            if (item.LimitedStack > 0)
                            {
                                Packet?.WriteInteger(item.LimitedStack);
                                Packet?.WriteInteger(item.LimitedStack - item.LimitedSells);

                            }
                        }

                        Packet?.WriteInteger(item.OnlyClub == true ? 1 : 0);
                        Packet?.WriteBoolean(false);
                        Packet?.WriteBoolean(false);
                        Packet?.WriteString($"{item.CatalogName!}.png");
                    }

                    Packet?.WriteInteger(0);
                    Packet?.WriteBoolean(false);
                    break;
                case "roomads":
                    Packet?.WriteInteger(2);
                    Packet?.WriteString(page.PageHeadline!);
                    Packet?.WriteString(page.PageTeaser!);
                    Packet?.WriteInteger(2);
                    Packet?.WriteString(page.PageTextOne ?? string.Empty);
                    Packet?.WriteString(page.PageTextTwo ?? string.Empty);
                    Packet?.WriteInteger(0);
                    Packet?.WriteInteger(-1);
                    Packet?.WriteBoolean(false);
                    break;
                case "pets2":
                case "pets3":
                    Packet?.WriteInteger(2);
                    Packet?.WriteString(page.PageHeadline!);
                    Packet?.WriteString(page.PageTeaser!);
                    Packet?.WriteInteger(4);
                    Packet?.WriteString(page.PageTextOne ?? string.Empty);
                    Packet?.WriteString(page.PageTextTwo ?? string.Empty);
                    Packet?.WriteString(page.PageTextDetails ?? string.Empty);
                    Packet?.WriteString(page.PageTextTeaser ?? string.Empty);
                    Packet?.WriteInteger(0);
                    Packet?.WriteInteger(-1);
                    Packet?.WriteBoolean(false);
                    break;
                case "frontpage4":
                    Packet?.WriteInteger(2);
                    Packet?.WriteString(page.PageHeadline!);
                    Packet?.WriteString(page.PageTeaser!);
                    Packet?.WriteInteger(3);
                    Packet?.WriteString(page.PageTextOne ?? string.Empty!);
                    Packet?.WriteString(page.PageTextTwo ?? string.Empty);
                    Packet?.WriteString(page.PageTextTeaser ?? string.Empty);
                    Packet?.WriteInteger(0);
                    Packet?.WriteInteger(-1);
                    Packet?.WriteBoolean(false);
                    Packet?.WriteInteger(0);
                    //foreach (var fPage in catalogsManager.FeaturedPages.Values.ToList())
                    //{
                    //    Packet?.WriteInteger(fPage.SlotId);
                    //    Packet?.WriteString(fPage.Caption!);
                    //    Packet?.WriteString(fPage.Image!);
                    //    Packet?.WriteInteger((int)fPage.Type);
                    //    switch (fPage.Type)
                    //    {
                    //        case FeaturePageTypes.PageName:
                    //            Packet?.WriteString(fPage.PageName!);
                    //            break;
                    //        case FeaturePageTypes.PageId:
                    //            Packet?.WriteInteger(fPage.PageId);
                    //            break;
                    //        case FeaturePageTypes.ProductName:
                    //            Packet?.WriteString(fPage.ProductName!);
                    //            break;
                    //    }
                    //    Packet?.WriteInteger(fPage.ExpireTimestamp != default ? (DateTime.Now - fPage.ExpireTimestamp)!.Value.Seconds : -1);
                    //}
                    break;
                default:
                    break;
            }
        }
    }
}