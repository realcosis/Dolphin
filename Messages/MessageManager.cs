using Microsoft.Extensions.Logging;
using RC.Common;
using RC.Common.Injection;
using Dolphin.Messages.Handler;
using Dolphin.Messages.Incoming;
using System.Collections.Concurrent;
using System.Reflection;
using Dolphin.Networking.Client;

namespace Dolphin.Messages
{
    [Scoped]
    class MessageManager(ILogger<IMessageManager> logger, IEnumerable<IIncomingHandler> incomingPackets) : IStartableService, IMessageManager
    {
        ConcurrentDictionary<short, IIncomingHandler> IMessageManager.Incomings { get; } = [];

        async Task IStartableService.Start()
        {
            await Task.Run(() =>
            {
                foreach (var packet in incomingPackets)
                {
                    var field = typeof(ClientPacketCode).GetField(packet.GetType().Name, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                    if (field != default)
                    {
                        var header = (short)field!.GetValue(null)!;
                        ((IMessageManager)this).Incomings.TryAdd(header, packet);
                    }
                    else
                        logger.LogWarning("No header was provided for {name}", packet);
                }
            });
        }

        async Task IMessageManager.Execute(ClientSession client, IncomingPacket packet)
        {
            if (client == default)
                return;

            if (!((IMessageManager)this).Incomings.TryGetValue(packet.Opcode, out IIncomingHandler? handler))
            {
                if (Emulator.Debug)
                    logger.LogDebug("Received unhandled packet with opcode {opcode}", packet.Opcode);
                return;
            }

            if (Emulator.Debug)
                logger.LogDebug("Received packet {name} [Opcode: {opcode}]", ((IMessageManager)this).Incomings[packet.Opcode].GetType().Name, packet.Opcode);

            if (handler == default)
                return;

            await handler!.Handle(client!, packet);
        }
    }
}