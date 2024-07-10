using Dolphin.Configurations;
using Dolphin.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Dolphin.DAL
{
    public class DolphinDbContext(IOptions<Configuration> config) : DbContext
    {
        readonly string connectionString = $"Server={config.Value!.Database?.Host};Database={config.Value!.Database?.Name};Uid={config.Value!.Database?.User};Pwd={config.Value!.Database?.Password};MinimumPoolSize={config.Value!.Database?.PoolMinSize};MaximumPoolSize={config.Value!.Database?.PoolMaxSize};ConvertZeroDateTime=True;";

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<UserTicketEntity> UserTickets { get; set; }

        public DbSet<UserBadgeEntity> UserBadges { get; set; }

        public DbSet<AchievementEntity> Achievements { get; set; }

        public DbSet<UserAchievementEntity> UserAchievements { get; set; }

        public DbSet<UserWardrobeEntity> UserWardrobes { get; set; }

        public DbSet<ItemUserEntity> ItemUsers { get; set; }

        public DbSet<ItemExtraDataEntity> ItemExtraDatas { get; set; }

        public DbSet<ItemLimitedEntity> ItemLimiteds { get; set; }

        public DbSet<ItemEntity> Items { get; set; }

        public DbSet<ItemBaseEntity> ItemBases { get; set; }

        public DbSet<VipEntity> Vips { get; set; }

        public DbSet<UserEffectEntity> UserEffects { get; set; }

        public DbSet<MessengerFriendshipEntity> MessengerFriendships { get; set; }

        public DbSet<MessengerRequestEntity> MessengerRequests { get; set; }

        public DbSet<RoomEntity> Rooms { get; set; }

        public DbSet<NavigatorFlatCatEntity> NavigatorFlatCats { get; set; }

        public DbSet<GroupMembershipEntity> GroupMemberships { get; set; }

        public DbSet<GroupEntity> Groups { get; set; }

        public DbSet<GroupElementEntity> GroupElements { get; set; }

        public DbSet<ChatlogPrivateEntity> ChatlogPrivates { get; set; }

        public DbSet<ChatlogPrivateDetailEntity> ChatlogPrivateDetails { get; set; }

        public DbSet<CatalogItemEntity> CatalogItems { get; set; }

        public DbSet<CatalogPageEntity> CatalogPages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasCharSet(CharSet.Utf8Mb4, DelegationModes.ApplyToAll);
            builder.Entity<MessengerFriendshipEntity>().HasOne(mf => mf.Sender).WithMany(u => u.SentFriends).HasForeignKey(mf => mf.SenderId);
            builder.Entity<MessengerFriendshipEntity>().HasOne(mf => mf.Receiver).WithMany(u => u.ReceivedFriends).HasForeignKey(mf => mf.ReceiverId);
            builder.Entity<MessengerRequestEntity>().HasOne(mr => mr.Sender).WithMany(u => u.SentRequests).HasForeignKey(mr => mr.SenderId);
            builder.Entity<MessengerRequestEntity>().HasOne(mr => mr.Receiver).WithMany(u => u.ReceivedRequests).HasForeignKey(mr => mr.ReceiverId);
            builder.Entity<UserIgnoreEntity>().HasOne(ui => ui.User).WithMany(u => u.IgnoredUsers).HasForeignKey(ui => ui.UserId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            builder.ConfigureMySQL(connectionString, options =>
            {
                options.EnableStringComparisonTranslations();
            });
            builder.UseSnakeCaseNamingConvention();
        }
    }
}