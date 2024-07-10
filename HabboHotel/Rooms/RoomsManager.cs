using Dolphin.DAL;
using Dolphin.HabboHotel.Rooms.Models.Navigators;
using Dolphin.HabboHotel.Rooms.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RC.Common;
using RC.Common.Injection;
using System.Collections.Concurrent;

namespace Dolphin.HabboHotel.Rooms
{
    [Scoped]
    class RoomsManager(DolphinDbContext dbContext, ILogger<IRoomsManager> logger) : IRoomsManager, IStartableService
    {
        ConcurrentDictionary<int, NavigatorCategory> IRoomsManager.NavigatorCategories { get; } = [];

        async Task IStartableService.Start()
        {
            ((IRoomsManager)this).NavigatorCategories.Clear();

            try
            {
                var categories = await dbContext.NavigatorFlatCats.AsNoTracking().ToListAsync();
                categories.ForEach(category => ((IRoomsManager)this).NavigatorCategories.TryAdd(category.Id, category.Map()));

                logger.LogInformation("RoomsManager has been loaded with {count} navigator categories definitions", ((IRoomsManager)this).NavigatorCategories.Count);
            }
            catch (Exception ex)
            {
                logger.LogWarning("Exception during starting of RoomsManager: {ex}", ex);
            }
        }
    }
}