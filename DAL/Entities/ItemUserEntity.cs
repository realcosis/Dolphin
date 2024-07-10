using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.ItemsUsers)]
    public class ItemUserEntity
    {
        [Key, Required]
        public int ItemId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity? User { get; set; }

        [ForeignKey(nameof(ItemId))]
        public ItemEntity? Item { get; set; }
    }
}