using Dolphin.DAL;
using Dolphin.HabboHotel.Groups.Models;
using Dolphin.HabboHotel.Groups.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Dolphin.Injection;
using System.Collections.Concurrent;

namespace Dolphin.HabboHotel.Groups
{
    [Scoped]
    class GroupsManager(IDbContextFactory<DolphinDbContext> dbContextFactory, DolphinDbContext dbContext, ILogger<IGroupsManager> logger) : IGroupsManager, IStartableService
    {
        ConcurrentDictionary<string, GroupElement> IGroupsManager.GroupElements { get; } = [];

        async Task<Group?> IGroupsManager.GetGroup(int groupId)
        {
            await using var dbContextInstance = await dbContextFactory.CreateDbContextAsync();
            var groupEntity = await dbContextInstance.Groups.AsNoTracking().Include(g => g.Members).ThenInclude(m => m.User).FirstOrDefaultAsync(g => g.Id == groupId);

            return groupEntity == default ? default : groupEntity.Map();
        }

        async Task IStartableService.Start()
        {
            ((IGroupsManager)this).GroupElements.Clear();

            try
            {
                var groupElements = await dbContext.GroupElements.AsNoTracking().ToListAsync();
                groupElements.ForEach(groupElement => ((IGroupsManager)this).GroupElements.TryAdd($"{groupElement.Id}-{groupElement.Type}", groupElement.Map()));

                logger.LogInformation("GroupsManager has been loaded with {count} group elements definitions", ((IGroupsManager)this).GroupElements.Count);
            }
            catch (Exception ex)
            {
                logger.LogWarning("Exception during starting of GroupsManager: {ex}", ex);
            }
        }
    }
}