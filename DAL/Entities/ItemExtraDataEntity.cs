using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.ItemExtraData)]
    public class ItemExtraDataEntity
    {
        [Key, Required]
        public int ItemId { get; set; }

        public string? Data { get; set; }

        [ForeignKey(nameof(ItemId))]
        public ItemEntity? Item { get; set; }
    }
}