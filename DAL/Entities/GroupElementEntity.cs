using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.GroupElements)]
    public class GroupElementEntity
    {
        [Required]
        public string? Type { get; set; }

        [Key]
        public int Id { get; set; }

        [Required, Column("ExtraData1")]
        public string? ExtraData1 { get; set; }

        [Required, Column("ExtraData2")]
        public string? ExtraData2 { get; set; }
    }
}