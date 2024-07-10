using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.UserEffects)]
    public class UserEffectEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int EffectId { get; set; } = 1;

        [Required]
        public int TotalDuration { get; set; } = 3600;

        [Required]
        public string IsActivated { get; set; } = "0";

        [Required]
        public double ActivatedStamp { get; set; } = 0;

        [ForeignKey(nameof(UserId))]
        public UserEntity? User { get; set; }
    }
}