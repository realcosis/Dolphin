using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.ItemLimited)]
    public class ItemLimitedEntity
    {
        [Key, Required]
        public int ItemId { get; set; }

        [Column("limitedno")]
        public int LimitedNo { get; set; }

        [Column("limitedtot")]
        public int LimitedTot { get; set; }

        [ForeignKey(nameof(ItemId))]
        public ItemEntity? Item { get; set; }
    }
}