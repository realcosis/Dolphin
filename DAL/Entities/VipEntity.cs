using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.Vips)]
    public class VipEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column("id_user")]
        public int UserId { get; set; }

        [Required, Column("timestamp")]
        public int Timestamp { get; set; }

        [Required, Column("timestampend")]
        public int TimestampEnd { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity? User { get; set; }
    }
}