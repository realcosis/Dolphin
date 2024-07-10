using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.Users)]
    [Index(nameof(Id), IsUnique = true)]
    public class UserEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string? Username { get; set; }

        public string? Motto { get; set; }

        public int Rank { get; set; }

        public string? Look { get; set; }

        public string? Gender { get; set; }

        public string? ChatPreference { get; set; }

        public string? FocusPreference { get; set; }

        public string? Volume { get; set; }

        public string? MentionEnabled { get; set; }

        public string? AlertEnabled { get; set; }

        public string? Online { get; set; }

        [Required]
        public DateTime? LastOnline { get; set; }

        public int Credits { get; set; }

        [Column("activity_points")]
        public int Duckets { get; set; }

        [Column("achievement_points")]
        public int AchievementScore { get; set; }

        public int Crystals { get; set; }

        [Required, Column("home_room")]
        public int HomeRoom { get; set; } = 0;

        public UserTicketEntity? Ticket { get; set; }

        public ICollection<UserFavoriteEntity> Favorites { get; set; } = [];

        public ICollection<UserWardrobeEntity> Wardrobes { get; set; } = [];

        public ICollection<UserBadgeEntity> Badges { get; set; } = [];

        public ICollection<UserAchievementEntity> Achievements { get; set; } = [];

        public ICollection<ItemUserEntity> Items { get; set; } = [];

        public ICollection<VipEntity> Vips { get; set; } = [];

        public ICollection<UserEffectEntity> Effects { get; set; } = [];

        public ICollection<MessengerFriendshipEntity> SentFriends { get; set; } = [];

        public ICollection<MessengerFriendshipEntity> ReceivedFriends { get; set; } = [];

        public ICollection<MessengerRequestEntity> SentRequests { get; set; } = [];

        public ICollection<MessengerRequestEntity> ReceivedRequests { get; set; } = [];

        public ICollection<GroupMembershipEntity> GroupMemberships { get; set; } = [];

        public ICollection<UserIgnoreEntity> IgnoredUsers { get; set; } = [];
    }
}