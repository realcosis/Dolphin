using Dolphin.HabboHotel.Catalogs.Models;
using Dolphin.Networking.Client;
using System.Collections.Concurrent;

namespace Dolphin.HabboHotel.Catalogs
{
    public interface ICatalogsManager
    {
        ConcurrentDictionary<int, CatalogPage> Pages { get; }

        ConcurrentDictionary<int, CatalogItem> Items { get; }

        Task HandlePurchase(CatalogItem item, ClientSession session, string extraData);
    }
}