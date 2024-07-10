using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.Achievements)]
    public class AchievementEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? GroupName { get; set; }

        [Required]
        public string? Category { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public int RewardPixels { get; set; }

        public int? RewardPoints { get; set; }

        [Required]
        public int ProgressNeeded { get; set; }
    }
}