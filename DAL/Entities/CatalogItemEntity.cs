using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.CatalogItems)]
    public class CatalogItemEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PageId { get; set; }

        [Required]
        public int ItemIds { get; set; }

        [Required]
        public string? CatalogName { get; set; }

        [Required]
        public int CostCredits { get; set; }

        [Required]
        public int CostPixels { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public int CostCrystal { get; set; }

        [Required, Column("cost_oude_belcredits")]
        public int CostOudeBelCredits { get; set; }

        [Required]
        public int SongId { get; set; }

        [Required]
        public int AllowMassbuy { get; set; }

        [Required]
        public int IsLimited { get; set; }

        [Required]
        public int InStock { get; set; }

        [Required]
        public int SoldAmount { get; set; }

        [Required]
        public string? Data { get; set; }

        [ForeignKey(nameof(ItemIds))]
        public ItemBaseEntity? ItemBase { get; set; }

        [ForeignKey(nameof(PageId))]
        public CatalogPageEntity? Page { get; set; }
    }
}