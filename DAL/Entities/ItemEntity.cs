using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.Items)]
    public class ItemEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }

        [Required]
        public int BaseId { get; set; }

        [ForeignKey(nameof(BaseId))]
        public ItemBaseEntity? ItemBase { get; set; }

        public ItemExtraDataEntity? ItemExtraData { get; set; }

        public ItemLimitedEntity? ItemLimited { get; set; }

        public ItemUserEntity? ItemUser { get; set; }
    }
}