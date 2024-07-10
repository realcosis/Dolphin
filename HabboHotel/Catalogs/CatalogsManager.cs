using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using RC.Common;
using RC.Common.Injection;
using Dolphin.DAL;
using Dolphin.HabboHotel.Catalogs.Models;
using Dolphin.HabboHotel.Catalogs.Mapper;
using Dolphin.Networking.Client;
using Dolphin.DAL.Entities;
using Dolphin.Messages.Outgoing.Inventory.Furni;
using Dolphin.Messages.Outgoing.Catalogs;
using Dolphin.HabboHotel.Users.Mapper;

namespace Dolphin.HabboHotel.Catalogs
{
    [Scoped]
    class CatalogsManager(DolphinDbContext dbContext, ILogger<ICatalogsManager> logger, IDbContextFactory<DolphinDbContext> dbContextFactory) : ICatalogsManager, IStartableService
    {
        ConcurrentDictionary<int, CatalogPage> ICatalogsManager.Pages { get; } = new();

        ConcurrentDictionary<int, CatalogItem> ICatalogsManager.Items { get; } = new();

        async Task ICatalogsManager.HandlePurchase(CatalogItem item, ClientSession session, string extraData)
        {
            await using var factoredDbContext = await dbContextFactory.CreateDbContextAsync();
            switch (item.ItemBase?.Type)
            {
                case "i":
                case "s":
                    switch (item.ItemBase?.InteractionType)
                    {
                        default:
                            var itemUserEntity = new ItemUserEntity
                            {
                                UserId = session.Habbo!.User!.Id
                            };
                            var itemLimitedEntity = new ItemLimitedEntity
                            {
                                LimitedTot = 0,
                                LimitedNo = 0
                            };
                            var itemExtraDataEntity = new ItemExtraDataEntity
                            {
                                Data = "0"
                            };
                            var itemEntitiy = new ItemEntity
                            {
                                BaseId = item.ItemBase!.Id,
                                ItemUser = itemUserEntity,
                                ItemLimited = itemLimitedEntity,
                                ItemExtraData = itemExtraDataEntity
                            };
                            var itemBase = await factoredDbContext.ItemBases.FirstOrDefaultAsync(ib => ib.ItemId == item.ItemBase!.Id);
                            if (itemBase != default)
                            {
                                await factoredDbContext.Items.AddAsync(itemEntitiy);
                                await factoredDbContext.SaveChangesAsync();
                                var userItem = itemUserEntity.Map(itemBase);
                                session.Habbo.Items.Add(userItem);
                                await session.Send(new FurniListNotificationMessageComposer(itemEntitiy.ItemId, 1));
                                await session.Send(new FurniListAddMessageComposer(userItem));
                            }
                            break;
                    }
                    break;
                case "r":
                    break;
                case "e":
                    break;
                default:
                    break;
            }
            await session.Send(new FurniListUpdateMessageComposer());
            await session.Send(new PurchaseOKMessageComposer(item));
        }

        async Task IStartableService.Start()
        {
            try
            {
                ((ICatalogsManager)this).Items.Clear();
                ((ICatalogsManager)this).Pages.Clear();

                var items = await dbContext.CatalogItems.AsNoTracking()
                                                         .Include(ci => ci.Page)
                                                         .Include(ci => ci.ItemBase)
                                                         .Where(ci => ci.Page != default && ci.ItemBase != default && ci.Amount > 0)
                                                         .ToListAsync();
                items.ForEach(item => ((ICatalogsManager)this).Items.TryAdd(item.Id, item.Map()));

                var pages = await dbContext.CatalogPages.AsNoTracking()
                                                         .Include(cp => cp.Items)
                                                            .ThenInclude(i => i.ItemBase)
                                                         .OrderBy(cp => cp.ParentId)
                                                         .ToListAsync();
                pages.ForEach(page => ((ICatalogsManager)this).Pages.TryAdd(page.Id, page.Map([.. page.Items])));

                logger.LogInformation("CatalogsManager has been loaded with {count} catalogs items definitions", ((ICatalogsManager)this).Items.Count);
                logger.LogInformation("CatalogsManager has been loaded with {count} catalogs pages definitions", ((ICatalogsManager)this).Pages.Count);
            }

            catch (Exception ex)
            {
                logger.LogWarning("Exception during starting of CatalogsManager: {ex}", ex);
            }
        }
    }
}