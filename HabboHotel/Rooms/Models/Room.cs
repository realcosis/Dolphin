namespace Dolphin.HabboHotel.Rooms.Models
{
    public class Room
    {
        public int Id { get; set; }

        public string? RoomType { get; set; } = "private";

        public string? Caption { get; set; } = "Room";

        public string? Owner { get; set; }

        public string? Description { get; set; }

        public int Category { get; set; } = 0;

        public string? State { get; set; } = "open";

        public int UsersNow { get; set; } = 0;

        public int UsersMax { get; set; } = 25;

        public string? ModelName { get; set; }

        public int Score { get; set; } = 0;

        public string? Tags { get; set; } = "";

        public int IconBg { get; set; } = 1;

        public int IconFg { get; set; } = 0;

        public string? IconItems { get; set; } = "0.0";

        public string? Password { get; set; } = "";

        public string? Wallpaper { get; set; } = "0.0";

        public string? Floor { get; set; } = "0.0";

        public string? Landscape { get; set; } = "0.0";

        public string? AllowPets { get; set; } = "1";

        public string? AllowPetsEat { get; set; } = "0";

        public string? AllowWalkthrough { get; set; } = "0";

        public string? AllowHideWall { get; set; } = "0";

        public string? AllowRightsOverride { get; set; } = "0";

        public int WallThickness { get; set; } = 0;

        public int FloorThickness { get; set; } = 0;

        public int GroupId { get; set; } = 0;

        public string? MuteSettings { get; set; } = "1";

        public string? BanSettings { get; set; } = "1";

        public string KickSettings { get; set; } = "1";

        public int BubbleOption { get; set; } = 1;

        public int BubbleSize { get; set; } = 0;

        public int Displacement { get; set; } = 1;

        public int DistanceListen { get; set; } = 14;

        public string? TradeState { get; set; }

        public int? WallHeight { get; set; } = 2;

        public int TradeSettings { get; set; } = 2;

        public int RollerSpeed { get; set; } = 4;

        public string HideWired { get; set; } = "0";
    }
}