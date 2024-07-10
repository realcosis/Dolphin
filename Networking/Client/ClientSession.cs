using Dolphin.HabboHotel.Users.Models;
using Dolphin.Messages;
using Dolphin.Messages.Handler;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;

namespace Dolphin.Networking.Client
{
    public class ClientSession(IChannelHandlerContext channel, ILogger<IMessageManager> logger)
    {
        public Guid SessionId { get; set; } = Guid.NewGuid();

        public Habbo? Habbo { get; set; }

        public async Task Send(OutgoingHandler message)
        {
            if (Emulator.Debug)
                logger.LogDebug("Sended packet {name} [Opcode: {opcode}]", message.GetType().Name, message.Id);
            await channel.WriteAndFlushAsync(message);
        }

        public async Task Dispose()
        {
            Habbo = null;
            await channel.DisconnectAsync();
        }
    }
}