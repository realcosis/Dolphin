using Dolphin.Core;
using Dolphin.HabboHotel.Catalogs;
using Dolphin.Messages.Handler;
using Dolphin.Messages.Outgoing.Inventory.Purse;
using Dolphin.Messages.Outgoing.Notifications;
using Dolphin.Networking.Client;
using Dolphin.Injection;

namespace Dolphin.Messages.Incoming.Catalogs
{
    [Singleton]
    class PurchaseFromCatalogMessageEvent(ICatalogsManager catalogsManager, ILanguageLocale languageLocale) : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
        {
            if (client.Habbo == default || client.Habbo?.User == default)
                return;

            var pageId = packet.ReadInt();
            var itemId = packet.ReadInt();
            var extraData = packet.ReadString();
            var amount = packet.ReadInt();

            var page = catalogsManager.Pages.Values.FirstOrDefault(p => p.Id == pageId);
            if (page == default)
                return;

            var item = page.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == default)
                return;

            if (amount < 1 || amount > 100)
                amount = 1;

            if (item.Amount > 1)
                amount = item.Amount;

            var totalCredits = amount > 1 ? ((item.CostCredits * amount) - ((int)Math.Floor((double)amount / 6) * item.CostCredits)) : item.CostCredits;
            var totalPoints = amount > 1 ? ((item.CostPoints * amount) - ((int)Math.Floor((double)amount / 6) * item.CostPoints)) : item.CostPoints;

            if (client.Habbo!.User!.Credits < totalCredits)
            {
                await client.Send(new MOTDNotificationMessageComposer(languageLocale.GetValue("catalog.credits.not_enough")));
                return;
            }

            if (item.PointsType == 0 && client.Habbo!.User!.Duckets < totalPoints)
            {
                await client.Send(new MOTDNotificationMessageComposer(languageLocale.GetValue($"catalog.points.not_enough.0")));
                return;
            }

            if (item.PointsType == 5 && client.Habbo!.User!.Crystals < totalPoints)
            {
                await client.Send(new MOTDNotificationMessageComposer(languageLocale.GetValue($"catalog.points.not_enough.5")));
                return;
            }

            await catalogsManager.HandlePurchase(item, client, extraData);

            if (item.CostCredits > 0)
            {
                client.Habbo.User.Credits -= totalCredits;
                await client.Send(new CreditBalanceMessageComposer(client.Habbo.User.Credits));
            }

            if (item.CostPoints > 0)
            {
                var currencyValue = 0;
                if (item.PointsType == 5)
                {
                    client.Habbo.User.Crystals -= totalPoints;
                    currencyValue = client.Habbo.User.Crystals;
                }                    
                else if (item.PointsType == 0)
                {
                    client.Habbo.User.Duckets -= totalPoints;
                    currencyValue = client.Habbo.User.Duckets;
                }
                await client.Send(new HabboActivityPointNotificationMessageComposer(currencyValue, 0, item.PointsType));
            }
        }
    }
}