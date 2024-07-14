using Dolphin.HabboHotel.Events;
using Dolphin.HabboHotel.Events.Models;
using Dolphin.Messages;
using Dolphin.Messages.Incoming;
using Dolphin.Networking.Client;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Dolphin.Networking.Handler
{
    class ClientHandler(IMessageManager messageManager, IEventsManager eventsManager, ILogger<IMessageManager> logger) : SimpleChannelInboundHandler<IncomingPacket>
    {
        readonly ConcurrentDictionary<IChannelId, ClientSession> clients = new();

        public override async void ChannelActive(IChannelHandlerContext context)
        {
            try
            {
                var client = new ClientSession(context, logger);

                if (!clients.TryAdd(context.Channel.Id, client))
                    await context.CloseAsync();
            }
            catch
            {

            }
        }

        public override async void ChannelInactive(IChannelHandlerContext context)
        {
            try
            {
                if (!clients.TryGetValue(context.Channel.Id, out ClientSession? client))
                    return;

                if (client.Habbo != default && client.Habbo!.User != default)
                    await eventsManager.TriggerEvent(EventTypes.USER_DISCONNECTED, new UserDisconnectedEvent
                    {
                        UserId = client.Habbo!.User!.Id,
                        Habbo = client.Habbo
                    });
                await context.CloseAsync();
                clients.TryRemove(context.Channel.Id, out _);
            }
            catch
            {

            }
        }

        protected override async void ChannelRead0(IChannelHandlerContext ctx, IncomingPacket msg)
        {
            try
            {
                if (clients.TryGetValue(ctx.Channel.Id, out ClientSession? client))
                    await messageManager.Execute(client, msg);
            }
            catch
            {
                
            }
        }

        public override void ChannelReadComplete(IChannelHandlerContext context)
            => context.Flush();
    }
}