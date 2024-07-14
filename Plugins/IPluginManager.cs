using Dolphin.HabboHotel.Rooms.Models.Navigators;
using System.Collections.Concurrent;

namespace Dolphin.Plugins
{
    public interface IPluginManager
    {
        HashSet<IPlugin> Plugins { get; }
    }
}