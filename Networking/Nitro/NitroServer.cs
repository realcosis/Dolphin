using DotNetty.Transport.Channels;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Codecs.Http;
using RC.Pixel.HL.Networking.Nitro.Codec;
using DotNetty.Handlers.Tls;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using DotNetty.Codecs;
using Dolphin.Configurations;
using Dolphin.Messages;
using Dolphin.Networking.Decoder;
using Dolphin.Networking.Encoder;
using Dolphin.HabboHotel.Events;
using Dolphin.Networking.Handler;

namespace Dolphin.Networking.Nitro
{
    class NitroServer(ILogger<NitroServer> logger, IOptions<Configuration> configuration,
                      IMessageManager messageManager, ILogger<IMessageManager> messageLogger, IEventsManager eventsManager) : Server(logger, configuration, 1, 12), IServer
    {
        async Task IServer.Start()
        {
            await Init();

            var certificate = default(X509Certificate2);
            if (configuration.Nitro!.SSL)
                certificate = new X509Certificate2("ssl/dotnetty.com.pfx", "password");

            serverBootstrap?.ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
            {
                if (configuration.Nitro!.SSL)
                    channel.Pipeline.AddLast(TlsHandler.Server(certificate));
                channel.Pipeline.AddLast(new HttpServerCodec());
                channel.Pipeline.AddLast(new HttpObjectAggregator(MAX_FRAME_SIZE));
                channel.Pipeline.AddLast(new WebSocketServerProtocolHandler("/", null, true, MAX_FRAME_SIZE, false, true));
                channel.Pipeline.AddLast(new WebSocketFrameCodec());
                channel.Pipeline.AddLast(new LengthFieldBasedFrameDecoder(MAX_FRAME_SIZE, 0, 4, 0, 4));
                channel.Pipeline.AddLast(new GamePolicyDecoder());
                channel.Pipeline.AddLast(new GameEncoder());
                channel.Pipeline.AddLast(new GameDecoder());
                channel.Pipeline.AddLast(new ClientHandler(messageManager, eventsManager, messageLogger));
            }));

            await Connect(configuration.Nitro!.Ip!, configuration.Nitro!.Port!, "NitroServer");
        }

        async Task IServer.Stop()
            => await Shutdown();
    }
}