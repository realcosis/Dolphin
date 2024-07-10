using Dolphin.DAL.Entities;
using Dolphin.HabboHotel.Catalogs.Models;
using Dolphin.HabboHotel.Items.Mapper;
using Dolphin.Util;

namespace Dolphin.HabboHotel.Catalogs.Mapper
{
    internal static class CatalogsMapper
    {
        internal static CatalogPage Map(this CatalogPageEntity entity, List<CatalogItemEntity> items)
            => new()
            {
                Id = entity.Id,
                ParentId = entity.ParentId,
                Caption = entity.Caption,
                PageLayout = entity.PageLayout,
                IconColor = entity.IconColor,
                IconImage = entity.IconImage,
                MinimumRank = entity.MinRank,
                OrderNumber = entity.OrderNum,
                Visible = entity.Visible.EnumToBool(),
                Enabled = entity.Enabled.EnumToBool(),
                PageHeadline = entity.PageHeadline,
                PageTeaser = entity.PageTeaser,
                PageSpecial = entity.PageSpecial,
                PageTextOne = entity.PageText1,
                PageTextTwo = entity.PageText2,
                PageTextTeaser = entity.PageTextTeaser,
                PageTextDetails = entity.PageTextDetails,
                Items = items.Select(i => i.Map()).ToList()
            };

        internal static CatalogItem Map(this CatalogItemEntity entity)
            => new()
            {
                Id = entity.Id,
                ItemId = entity.ItemIds,
                CatalogName = entity.ItemBase?.PublicName,
                Amount = entity.Amount,
                CostCredits = entity.CostCredits,
                CostPoints = entity.CostCrystal > 0 ? entity.CostCrystal : entity.CostPixels,
                PointsType = entity.CostCrystal > 0 ? 5 : 0,
                Extradata = entity.Data,
                LimitedSells = entity.SoldAmount,
                LimitedStack = entity.InStock,
                PageId = entity.PageId,
                SongId = entity.SongId,
                IsLimited = entity.IsLimited.EnumToBool(),
                ItemBase = entity.ItemBase?.Map()
            };
    }
}