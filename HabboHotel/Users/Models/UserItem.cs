using Dolphin.HabboHotel.Items.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.HabboHotel.Users.Models
{
    public class UserItem
    {
        public int ItemId { get; set; }

        public string? ExtraData { get; set; }

        public int ItemBaseId { get; set; }

        public int LimitedNo { get; set; } = 0;

        public int LimitedTot { get; set; } = 0;

        public Item? ItemBase { get; set; }
    }
}