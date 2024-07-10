using Dolphin.HabboHotel.Rooms.Models.Navigators;
using System.Collections.Concurrent;

namespace Dolphin.HabboHotel.Rooms
{
    public interface IRoomsManager
    {
        ConcurrentDictionary<int, NavigatorCategory> NavigatorCategories { get; }
    }
}