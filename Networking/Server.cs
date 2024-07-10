using Dolphin.Configurations;
using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Dolphin.Injection;
using System.Net;

namespace Dolphin.Networking
{
    [Scoped]
    public abstract class Server(ILogger<Server> logger, IOptions<Configuration> configuration, int bossGroupThreads, int workerGroupThreads)
    {
        protected string? name;
        protected IChannel? serverChannel;
        public const int MAX_FRAME_SIZE = 500000;
        protected ServerBootstrap? serverBootstrap;
        readonly MultithreadEventLoopGroup bossGroup = new(bossGroupThreads);
        readonly MultithreadEventLoopGroup workerGroup = new(workerGroupThreads);
        protected readonly Configuration configuration = configuration.Value;

        public async Task<ServerBootstrap> Init()
        {
            return await Task.FromResult(serverBootstrap = new ServerBootstrap()
                .Group(bossGroup, workerGroup)
                .Channel<TcpServerSocketChannel>()
                .ChildOption(ChannelOption.TcpNodelay, true)
                .ChildOption(ChannelOption.SoKeepalive, true)
                .ChildOption(ChannelOption.SoReuseaddr, true)
                .ChildOption(ChannelOption.SoRcvbuf, 4096)
                .ChildOption(ChannelOption.RcvbufAllocator, new FixedRecvByteBufAllocator(4096))
                .ChildOption(ChannelOption.Allocator, UnpooledByteBufferAllocator.Default));
        }

        public async Task Connect(string host, int port, string serverName)
        {
            if (serverBootstrap == default)
                return;

            name = serverName;
            serverChannel = await serverBootstrap.BindAsync(IPAddress.Parse(host), port);

            if (!serverChannel.IsWritable)
                logger.LogError("Failed to connect to host {host}:{port} [{name}]", host, port, name);
            else
                logger.LogInformation("Started Server on {host}:{port} [{name}]", host, port, name);
        }

        public async Task Shutdown()
        {
            if (serverChannel == default)
                return;

            await serverChannel.CloseAsync();
            ShutdownWorkers();
            logger.LogInformation("{name} has been shutdowned", name);
        }

        public void ShutdownWorkers()
        {
            bossGroup.ShutdownGracefullyAsync();
            workerGroup.ShutdownGracefullyAsync();
        }
    }
}