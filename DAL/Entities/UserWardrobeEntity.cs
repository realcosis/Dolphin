using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.UserWardrobe)]
    public class UserWardrobeEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int SlotId { get; set; }

        [Required]
        public string? Look { get; set; }

        [Required]
        public string? Gender { get; set; } = "M";

        [ForeignKey(nameof(UserId))]
        public UserEntity? User { get; set; }
    }
}