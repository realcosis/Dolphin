using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.UserAchievement)]
    public class UserAchievementEntity
    {
        [Required, Column("userid"), Key]
        public int UserId { get; set; }

        [Required]
        public string? Group { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public int Progress { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity? User { get; set; }
    }
}