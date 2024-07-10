using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.CatalogPages)]
    public class CatalogPageEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ParentId { get; set; }

        [Required]
        public string? Caption { get; set; }

        [Required]
        public int IconColor { get; set; }

        [Required]
        public int IconImage { get; set; }

        [Required]
        public string? Visible { get; set; }

        [Required]
        public string? Enabled { get; set; }

        [Required]
        public int MinRank { get; set; }

        [Required]
        public string? ClubOnly { get; set; }

        [Required]
        public string? ComingSoon { get; set; }

        [Required]
        public int OrderNum { get; set; }

        [Required]
        public string? PageLayout { get; set; }

        [Required]
        public string? PageHeadline { get; set; }

        [Required]
        public string? PageTeaser { get; set; }

        [Required]
        public string? PageSpecial { get; set; }

        [Required]
        public string? PageText1 { get; set; }

        [Required]
        public string? PageText2 { get; set; }

        [Required]
        public string? PageTextDetails { get; set; }

        [Required]
        public string? PageTextTeaser { get; set; }

        public string? Text3 { get; set; }

        public string? Link { get; set; }

        public string? PageContent { get; set; }

        [Column("shouldlog")]
        public int? ShouldLog { get; set; }

        public ICollection<CatalogItemEntity> Items { get; set; } = [];
    }
}