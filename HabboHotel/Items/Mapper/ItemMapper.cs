using Dolphin.DAL.Entities;
using Dolphin.HabboHotel.Items.Models;

namespace Dolphin.HabboHotel.Items.Mapper
{
    internal static class ItemMapper
    {
        internal static Item Map(this ItemBaseEntity entity)
            => new()
            {
                Id = entity.ItemId,
                SpriteId = entity.SpriteId,
                AllowGift = entity.AllowGift,
                AllowInventoryStack = entity.AllowInventoryStack,
                AllowMarketplaceSell = entity.AllowMarketplaceSell,
                AllowRecycle = entity.AllowRecycle,
                AllowSit = entity.AllowSit,
                AllowStack = entity.AllowStack,
                StackHeight = entity.Height,
                AllowTrade = entity.AllowTrade,
                AllowWalk = entity.AllowWalk,
                InteractionType = entity.InteractionType!.GetTypeFromString(),
                InteractionModesCount = entity.CycleCount,
                ItemName = entity.ItemName,
                Length = entity.Length,
                PublicName = entity.PublicName,
                Type = entity.Type,
                VendingIds = entity.VendingIds,
                Width = entity.Width
            };
    }
}