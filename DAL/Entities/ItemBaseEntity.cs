using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.ItemBase)]
    public class ItemBaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }

        [Required]
        public int SpriteId { get; set; }

        [Required]
        public string? PublicName { get; set; }

        [Required]
        public string? ItemName { get; set; }

        [Required]
        public string? Type { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Length { get; set; }

        [Required]
        public double Height { get; set; }

        [Required]
        public bool AllowStack { get; set; }

        [Required]
        public bool AllowWalk { get; set; }

        [Required]
        public bool AllowSit { get; set; }

        [Required]
        public bool AllowRecycle { get; set; }

        [Required]
        public bool AllowTrade { get; set; }

        [Required]
        public bool AllowMarketplaceSell { get; set; }

        [Required]
        public bool AllowGift { get; set; }

        [Required]
        public bool AllowInventoryStack { get; set; }

        [Required]
        public string? InteractionType { get; set; }

        [Required]
        public int CycleCount { get; set; }

        [Required]
        public string VendingIds { get; set; } = "0";

        [Required, Column("is_groupitem")]
        public bool IsGroupItem { get; set; }

        [Column("partcolors")]
        public string? PartColors { get; set; }

        public int? Revision { get; set; }

        public string? Description { get; set; }

        public ICollection<ItemEntity> Items { get; set; } = [];
    }
}