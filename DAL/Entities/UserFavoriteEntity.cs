using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.UserFavorites)]
    public class UserFavoriteEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity? User { get; set; }

        [ForeignKey(nameof(RoomId))]
        public RoomEntity? Room { get; set; }
    }
}