using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.Rooms)]
    [Index(nameof(Id), IsUnique = true)]
    [Index(nameof(Owner))]
    public class RoomEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column("roomtype")]
        public string? RoomType { get; set; } = "private";

        [Required]
        public string? Caption { get; set; } = "Room";

        [Required]
        public string? Owner { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public int Category { get; set; } = 0;

        [Required]
        public string? State { get; set; } = "open";

        [Required]
        public int UsersNow { get; set; } = 0;

        [Required]
        public int UsersMax { get; set; } = 25;

        [Required]
        public string? ModelName { get; set; }

        [Required]
        public int Score { get; set; } = 0;

        [Required]
        public string? Tags { get; set; } = "";

        [Required]
        public int IconBg { get; set; } = 1;

        [Required]
        public int IconFg { get; set; } = 0;

        [Required]
        public string? IconItems { get; set; } = "0.0";

        [Required]
        public string? Password { get; set; } = "";

        [Required]
        public string? Wallpaper { get; set; } = "0.0";

        [Required]
        public string? Floor { get; set; } = "0.0";

        [Required]
        public string? Landscape { get; set; } = "0.0";

        [Required]
        public string? AllowPets { get; set; } = "1";

        [Required]
        public string? AllowPetsEat { get; set; } = "0";

        [Required]
        public string? AllowWalkthrough { get; set; } = "0";

        [Required, Column("allow_hidewall")]
        public string? AllowHideWall { get; set; } = "0";

        [Required, Column("allow_rightsoverride")]
        public string? AllowRightsOverride { get; set; } = "0";

        [Required, Column("wallthickness")]
        public int WallThickness { get; set; } = 0;

        [Required, Column("floorthickness")]
        public int FloorThickness { get; set; } = 0;

        [Required]
        public int GroupId { get; set; } = 0;

        [Required]
        public string? MuteSettings { get; set; } = "1";

        public string? BanSettings { get; set; } = "1";

        [Required]
        public string KickSettings { get; set; } = "1";

        [Required]
        public int BubbleOption { get; set; } = 1;

        [Required]
        public int BubbleSize { get; set; } = 0;

        [Required]
        public int Displacement { get; set; } = 1;

        [Required]
        public int DistanceListen { get; set; } = 14;

        public string? TradeState { get; set; }

        [Column("wallheight")]
        public int? WallHeight { get; set; } = 2;

        [Required]
        public int TradeSettings { get; set; } = 2;

        [Required]
        public int RollerSpeed { get; set; } = 4;

        public string HideWired { get; set; } = "0";

        public ICollection<UserFavoriteEntity> Favorites { get; set; } = [];
    }
}