using Dolphin.DAL.Enums;

namespace Dolphin.HabboHotel.Items.Models
{
    public class Item
    {
        public int Id { get; set; }

        public int SpriteId { get; set; }

        public string? PublicName { get; set; }

        public string? ItemName { get; set; }

        public string? Type { get; set; }

        public int Width { get; set; }

        public int Length { get; set; }

        public double StackHeight { get; set; }

        public bool AllowStack { get; set; }

        public bool AllowSit { get; set; }

        public bool AllowWalk { get; set; }

        public bool AllowGift { get; set; }

        public bool AllowTrade { get; set; }

        public bool AllowRecycle { get; set; }

        public bool AllowMarketplaceSell { get; set; }

        public bool AllowInventoryStack { get; set; }

        public InteractionTypes? InteractionType { get; set; }

        public int InteractionModesCount { get; set; }

        public string? VendingIds { get; set; }
    }
}