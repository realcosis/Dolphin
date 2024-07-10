using Dolphin.HabboHotel.Achievements.Models;
using System.Collections.Concurrent;

namespace Dolphin.HabboHotel.Achievements
{
    public interface IAchievementsManager
    {
        ConcurrentDictionary<string, Achievement> Achievements { get; }
    }
}