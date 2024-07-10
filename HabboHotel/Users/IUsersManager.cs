using Dolphin.HabboHotel.Users.Models;
using System.Collections.Concurrent;

namespace Dolphin.HabboHotel.Users
{
    public interface IUsersManager
    {
        ConcurrentDictionary<int, Habbo> Users { get; }

        Task<Habbo?> GetHabbo(int userId);

        Task<Habbo?> LoadHabbo(string sessionTicket);

        Task GiveBadge(int userId, UserBadge badge);

        Task<bool> HasBadge(int userId, string badgeCode);

        Task RemoveBadge(int userId, UserBadge badge);

        Task UpsertSlot(int userId, UserWardrobe wardrobe, bool newWardrobe);
    }
}