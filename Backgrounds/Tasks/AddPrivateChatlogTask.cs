using Dolphin.DAL;
using Dolphin.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Dolphin.Injection;

namespace Dolphin.Backgrounds.Tasks
{
    [Singleton]
    class AddPrivateChatlogTask(IDbContextFactory<DolphinDbContext> dbContextFactory) : ITask
    {
        public object[] Parameters { get; set; } = [];

        async Task ITask.Execute()
        {
            if (Parameters == default)
                return;

            if (!int.TryParse(Parameters[0].ToString(), out var fromId))
                return;

            if (!int.TryParse(Parameters[1].ToString(), out var toId))
                return;

            var message = Parameters[2].ToString();

            await using var dbContext = await dbContextFactory.CreateDbContextAsync();
            var privateChatlog = await dbContext.ChatlogPrivates.AsNoTracking()
                                                                .FirstOrDefaultAsync(cp => (cp.FromId == fromId && cp.ToId == toId) || (cp.FromId == toId && cp.ToId == fromId));
            if (privateChatlog == default)
            {
                privateChatlog = new ChatlogPrivateEntity
                {
                    FromId = fromId,
                    ToId = toId
                };
                await dbContext.ChatlogPrivates.AddAsync(privateChatlog);
                await dbContext.SaveChangesAsync();
            }
            var entity = new ChatlogPrivateDetailEntity
            {
                ChatlogId = privateChatlog.Id,
                Message = message,
                Timestamp = DateTime.Now,
                UserId = fromId
            };
            await dbContext.ChatlogPrivateDetails.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}