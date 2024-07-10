using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Procedures
{
    public class GetUserItemProcedure
    {
        public int ItemId { get; set; }

        public int BaseId { get; set; }

        [Column("data")]
        public string? ExtraData { get; set; }

        [Column("limitedno")]
        public int? LimitedNo { get; set; }

        [Column("limitedtot")]
        public int? LimitedTot { get; set; }
    }
}