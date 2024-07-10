using Dolphin.HabboHotel.Items.Models;

namespace Dolphin.HabboHotel.Catalogs.Models
{
    public class CatalogItem
    {
        public int Id { get; set; }

        public int? ItemId { get; set; }

        public int PageId { get; set; }

        public string? CatalogName { get; set; }

        public int CostCredits { get; set; }

        public int CostPoints { get; set; }

        public int PointsType { get; set; }

        public int Amount { get; set; }

        public int LimitedStack { get; set; }

        public int LimitedSells { get; set; }

        public int OrderNumber { get; set; }

        public int OfferId { get; set; }

        public int SongId { get; set; }

        public string? Extradata { get; set; }

        public bool HaveOffer { get; set; }

        public bool OnlyClub { get; set; }

        public Item? ItemBase { get; set; }

        public bool IsLimited { get; set; }
    }
}