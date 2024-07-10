using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.NavigatorFlatCats)]
    public class NavigatorFlatCatEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Caption { get; set; }

        public string Enabled { get; set; } = "1";

        public int MinRank { get; set; } = 1;
    }
}