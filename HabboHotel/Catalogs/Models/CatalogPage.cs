namespace Dolphin.HabboHotel.Catalogs.Models
{
    public class CatalogPage
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public string? Caption { get; set; }

        public string? PageLayout { get; set; }

        public int IconColor { get; set; }

        public int IconImage { get; set; }

        public int MinimumRank { get; set; }

        public int OrderNumber { get; set; }

        public bool Visible { get; set; }

        public bool Enabled { get; set; }

        public string? PageHeadline { get; set; }

        public string? PageTeaser { get; set; }

        public string? PageSpecial { get; set; }

        public string? PageTextOne { get; set; }

        public string? PageTextTwo { get; set; }

        public string? PageTextDetails { get; set; }

        public string? PageTextTeaser { get; set; }

        public List<CatalogItem> Items { get; set; } = [];
    }
}