using Dolphin.DAL;
using Dolphin.HabboHotel.Achievements.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RC.Common;
using RC.Common.Injection;
using System.Collections.Concurrent;

namespace Dolphin.HabboHotel.Achievements
{
    [Scoped]
    class AchievementsManager(DolphinDbContext dbContext, ILogger<IAchievementsManager> logger) : IStartableService, IAchievementsManager
    {
        ConcurrentDictionary<string, Achievement> IAchievementsManager.Achievements { get; } = [];

        async Task IStartableService.Start()
        {
            ((IAchievementsManager)this).Achievements.Clear();

            try
            {
                var achievements = await dbContext.Achievements.ToListAsync();
                foreach (var achievement in achievements)
                {
                    var level = new AchievementLevel
                    {
                        Level = achievement.Level,
                        Requirement = achievement.ProgressNeeded,
                        RewardPixels = achievement.RewardPixels,
                        RewardPoints = achievement.RewardPoints
                    };
                    if (((IAchievementsManager)this).Achievements.TryGetValue(achievement.GroupName!, out var savedAchievement))
                        savedAchievement.AddLevel(level);
                    else
                    {
                        var achievementData = new Achievement
                        {
                            GroupName = achievement.GroupName,
                            Category = achievement.Category,
                            Id = achievement.Id
                        };
                        achievementData.AddLevel(level);
                        ((IAchievementsManager)this).Achievements.TryAdd(achievement.GroupName!, achievementData);
                    }
                }

                logger.LogInformation("AchievementsManager has been loaded with {count} achievements definitions", ((IAchievementsManager)this).Achievements.Count);
            }
            catch (Exception ex)
            {
                logger.LogWarning("Exception during starting of AchievementsManager: {ex}", ex);
            }
        }
    }
}