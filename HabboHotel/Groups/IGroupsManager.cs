using Dolphin.HabboHotel.Groups.Models;
using System.Collections.Concurrent;

namespace Dolphin.HabboHotel.Groups
{
    public interface IGroupsManager
    {
        ConcurrentDictionary<string, GroupElement> GroupElements { get; }

        Task<Group?> GetGroup(int groupId);
    }
}