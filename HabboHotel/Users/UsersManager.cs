using Dolphin.DAL;
using Dolphin.HabboHotel.Events;
using Dolphin.HabboHotel.Events.Models;
using Dolphin.HabboHotel.Groups.Mappers;
using Dolphin.HabboHotel.Users.Mapper;
using Dolphin.HabboHotel.Users.Models;
using Dolphin.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RC.Common.Injection;
using System.Collections.Concurrent;
using Dolphin.HabboHotel.Rooms.Mappers;
using Dolphin.Messages.Outgoing.Messenger;
using RC.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Dolphin.HabboHotel.Users
{
    [Scoped]
    class UsersManager(IDbContextFactory<DolphinDbContext> dbContextFactory, IEventsManager eventsManager, ILogger<IUsersManager> logger) : IUsersManager, IDisposableService
    {
        ConcurrentDictionary<int, Habbo> IUsersManager.Users { get; } = [];

        #region Badges

        async Task IUsersManager.GiveBadge(int userId, UserBadge badge)
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();
            if (!await ((IUsersManager)this).HasBadge(userId, badge.Code!) && ((IUsersManager)this).Users.TryGetValue(userId, out var habbo))
            {
                habbo.Badges.Add(badge);
                await dbContext.UserBadges.AddAsync(badge.Map(userId));
                await dbContext.SaveChangesAsync();
            }
        }

        async Task<bool> IUsersManager.HasBadge(int userId, string badgeCode)
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();
            var hasBadge = false;
            if (((IUsersManager)this).Users.TryGetValue(userId, out var habbo))
                hasBadge = habbo.Badges.Any(b => b.Code == badgeCode);
            return await Task.FromResult(hasBadge);
        }

        async Task IUsersManager.RemoveBadge(int userId, UserBadge badge)
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();
            if (await ((IUsersManager)this).HasBadge(userId, badge.Code!) && ((IUsersManager)this).Users.TryGetValue(userId, out var habbo))
            {
                habbo.Badges.Remove(badge);
                var badgeEntity = await dbContext.UserBadges.FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BadgeId == badge.Code);
                if (badgeEntity != default)
                {
                    dbContext.UserBadges.Remove(badgeEntity);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        #endregion

        #region Wardrobe

        async Task IUsersManager.UpsertSlot(int userId, UserWardrobe wardrobe, bool newWardrobe)
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();
            if (newWardrobe)
                await dbContext.UserWardrobes.AddAsync(wardrobe.MapAdd(userId));
            else
            {
                var wardrobeEntity = await dbContext.UserWardrobes.FirstOrDefaultAsync(uw => uw.UserId == userId && uw.SlotId == wardrobe.SlotId);
                if (wardrobeEntity != default)
                    dbContext.UserWardrobes.Update(wardrobeEntity.MapUpdate(wardrobe));
            }
            await dbContext.SaveChangesAsync();
        }

        #endregion

        #region Init

        async Task<Habbo?> IUsersManager.GetHabbo(int userId)
        {
            try
            {
                await using var dbContext = await dbContextFactory.CreateDbContextAsync();
                var user = await dbContext.Users
                                            .Include(u => u.Wardrobes)
                                            .Include(u => u.Badges)
                                            .Include(u => u.GroupMemberships)
                                                .ThenInclude(u => u.Group)
                                            .Include(u => u.Vips)
                                            .Include(u => u.Effects)
                                            .Include(u => u.Favorites)
                                                .ThenInclude(f => f.Room)
                                            .Include(u => u.IgnoredUsers)
                                                .ThenInclude(iu => iu.IgnoredUser)
                                            .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == default)
                    return default;

                var achievements = await dbContext.UserAchievements.AsNoTracking().Where(ua => ua.UserId == user.Id).ToListAsync();

                var items = await dbContext.ItemUsers
                                            .Where(iu => iu.UserId == user.Id)
                                            .AsNoTracking()
                                            .Include(iu => iu.Item)
                                                .ThenInclude(i => i!.ItemBase)
                                            .Include(iu => iu.Item)
                                                .ThenInclude(i => i!.ItemExtraData)
                                            .Include(iu => iu.Item)
                                                .ThenInclude(i => i!.ItemLimited)
                                            .ToListAsync();

                var friends = (await dbContext.MessengerFriendships.AsNoTracking()
                                                                   .Include(mf => mf.Receiver)
                                                                   .Where(mf => mf.SenderId == user.Id)
                                                                   .ToListAsync()).Select(mf => mf.MapReceiver()).Union((await dbContext.MessengerFriendships.AsNoTracking()
                                                                   .Include(mf => mf.Sender)
                                                                   .Where(mf => mf.ReceiverId == user.Id)
                                                                   .ToListAsync()).Select(mf => mf.MapSender())).ToList();

                var requests = (await dbContext.MessengerRequests.AsNoTracking()
                                                                 .Include(mf => mf.Sender)
                                                                 .Where(mf => mf.ReceiverId == user.Id)
                                                                 .ToListAsync()).Select(mf => mf.MapSender()).ToList();

                var rooms = (await dbContext.Rooms.Where(r => r.Owner == user.Username).ToListAsync()).Select(r => r.Map()).ToList();

                var habbo = new Habbo
                {
                    User = user.Map(),
                    Badges = user.Badges.Select(b => b.Map()).ToList(),
                    Achievements = achievements.Select(a => a.Map()).ToList(),
                    Wardrobes = user.Wardrobes.Select(w => w.Map()).ToList(),
                    Items = items.Select(i => i.Map()).ToList(),
                    IsVip = user.Vips.Any(v => TextHandling.GetNow() <= v.TimestampEnd),
                    Friends = friends,
                    Requests = requests,
                    Groups = user.GroupMemberships.Select(gm => gm.Group).Select(g => g!.Map()).ToList(),
                    FavoriteRooms = user.Favorites.Select(f => f.Room).Select(r => r!.Map()).ToList(),
                    Rooms = rooms,
                    IgnoredUsers = user.IgnoredUsers.Select(iu => iu.Map()).ToList(),
                    Effects = user.Effects.Select(e => e.Map()).ToList()
                };

                return habbo;
            }
            catch (Exception ex)
            {
                logger.LogError("Exception during loading of user with userid {userid}: {ex}", userId, ex);
                return default;
            }
        }

        async Task<Habbo?> IUsersManager.LoadHabbo(string sessionTicket)
        {
            try
            {
                await using var dbContext = await dbContextFactory.CreateDbContextAsync();
                var user = await dbContext.Users.Include(u => u.Ticket)
                                                .Include(u => u.Wardrobes)
                                                .Include(u => u.Badges)
                                                .Include(u => u.GroupMemberships)
                                                    .ThenInclude(u => u.Group)
                                                .Include(u => u.Vips)
                                                .Include(u => u.Effects)
                                                .Include(u => u.Favorites)
                                                    .ThenInclude(f => f.Room)
                                                .Include(u => u.IgnoredUsers)
                                                    .ThenInclude(iu => iu.IgnoredUser)
                                                .FirstOrDefaultAsync(u => u.Ticket!.SessionTicket!.Equals(sessionTicket, StringComparison.CurrentCultureIgnoreCase));

                if (user == default)
                    return default;

                user.Online = "1";
                user.LastOnline = DateTime.Now;
                dbContext.Users.Update(user);
                await dbContext.SaveChangesAsync();

                var achievements = await dbContext.UserAchievements.AsNoTracking().Where(ua => ua.UserId == user.Id).ToListAsync();

                var items = await dbContext.ItemUsers
                                            .Where(iu => iu.UserId == user.Id)
                                            .AsNoTracking()
                                            .Include(iu => iu.Item)
                                                .ThenInclude(i => i!.ItemBase)
                                            .Include(iu => iu.Item)
                                                .ThenInclude(i => i!.ItemExtraData)
                                            .Include(iu => iu.Item)
                                                .ThenInclude(i => i!.ItemLimited)
                                            .ToListAsync();

                var friends = (await dbContext.MessengerFriendships.AsNoTracking()
                                                                   .Include(mf => mf.Receiver)
                                                                   .Where(mf => mf.SenderId == user.Id)
                                                                   .ToListAsync()).Select(mf => mf.MapReceiver()).Union((await dbContext.MessengerFriendships.AsNoTracking()
                                                                   .Include(mf => mf.Sender)
                                                                   .Where(mf => mf.ReceiverId == user.Id)
                                                                   .ToListAsync()).Select(mf => mf.MapSender())).ToList();

                var requests = (await dbContext.MessengerRequests.AsNoTracking()
                                                                 .Include(mf => mf.Sender)
                                                                 .Where(mf => mf.ReceiverId == user.Id)
                                                                 .ToListAsync()).Select(mf => mf.MapSender()).ToList();

                var rooms = (await dbContext.Rooms.Where(r => r.Owner == user.Username).ToListAsync()).Select(r => r.Map()).ToList();

                var habbo = new Habbo
                {
                    User = user.Map(),
                    Badges = user.Badges.Select(b => b.Map()).ToList(),
                    Achievements = achievements.Select(a => a.Map()).ToList(),
                    Wardrobes = user.Wardrobes.Select(w => w.Map()).ToList(),
                    Items = items.Select(i => i.Map()).ToList(),
                    IsVip = user.Vips.Any(v => TextHandling.GetNow() <= v.TimestampEnd),
                    Friends = friends,
                    Requests = requests,
                    Groups = user.GroupMemberships.Select(gm => gm.Group).Select(g => g!.Map()).ToList(),
                    FavoriteRooms = user.Favorites.Select(f => f.Room).Select(r => r!.Map()).ToList(),
                    Rooms = rooms,
                    IgnoredUsers = user.IgnoredUsers.Select(iu => iu.Map()).ToList(),
                    Effects = user.Effects.Select(e => e.Map()).ToList()
                };

                await Parallel.ForEachAsync(habbo.Friends, async (item, token) =>
                {
                    if (((IUsersManager)this).Users.TryGetValue(item.UserId, out var friendHabbo))
                    {
                        var buddy = friendHabbo.Friends.FirstOrDefault(f => f.UserId == habbo.User.Id);
                        if (buddy != default)
                        {
                            buddy.Online = "1";
                            await friendHabbo.ClientSession?.Send(new FriendListUpdateMessageComposer(buddy, default, 1, []))!;
                        }
                    }
                });

                if (Emulator.Debug)
                    logger.LogInformation("{name} has logged in", habbo.User!.Username);

                ((IUsersManager)this).Users.TryAdd(habbo.User!.Id, habbo);

                await eventsManager.RegisterListener(EventTypes.USER_DISCONNECTED, async (eventData) => await OnDisconnect((UserDisconnectedEvent)eventData));

                return habbo;
            }
            catch (Exception ex)
            {
                logger.LogError("Exception during loading of user with session ticket {sso}: {ex}", sessionTicket, ex);
                return default;
            }
        }

        #endregion

        #region Dispose 

        async Task IDisposableService.Dispose()
        {
            await Parallel.ForEachAsync(((IUsersManager)this).Users.Values, async (item, token) =>
            {
                await eventsManager.TriggerEvent(EventTypes.USER_DISCONNECTED, new UserDisconnectedEvent
                {
                    UserId = item.User!.Id
                });
            });
            logger.LogInformation("{name} has been shutdowned", nameof(UsersManager));
        }

        #endregion

        #region Private Methods

        async Task OnDisconnect(UserDisconnectedEvent @event)
        {
            try
            {
                if (((IUsersManager)this).Users.TryGetValue(@event.UserId, out var habbo))
                {
                    await using var dbContext = await dbContextFactory.CreateDbContextAsync();
                    var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == habbo.User!.Id);
                    if (user != default)
                    {
                        user.Online = "0";
                        user.Credits = habbo.User!.Credits;
                        user.Duckets = habbo.User!.Duckets!;
                        user.Crystals = habbo.User!.Crystals!;
                        dbContext.Users.Update(user);
                        await dbContext.SaveChangesAsync();
                        await Parallel.ForEachAsync(habbo.Friends, async (item, token) =>
                        {
                            if (((IUsersManager)this).Users.TryGetValue(item.UserId, out var friendHabbo))
                            {
                                var buddy = friendHabbo.Friends.FirstOrDefault(f => f.UserId == habbo.User!.Id);
                                if (buddy != default)
                                {
                                    buddy.Online = "0";
                                    await friendHabbo.ClientSession?.Send(new FriendListUpdateMessageComposer(default, habbo.User!.Id, -1, []))!;
                                }
                            }
                        });
                        await habbo.ClientSession!.Dispose();
                        if (Emulator.Debug)
                            logger.LogInformation("{name} has logged out", habbo.User!.Username);
                        ((IUsersManager)this).Users.TryRemove(habbo.User!.Id, out _);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Exception during disconnection of user {ex}", ex);
            }
        }

        #endregion
    }
}