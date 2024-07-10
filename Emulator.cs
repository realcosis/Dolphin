using Dolphin.Networking;
using Microsoft.Extensions.Logging;
using RC.Common;
using RC.Common.Injection;
using System.Diagnostics;
using System.Globalization;

namespace Dolphin
{
    [Scoped]
    class Emulator(ILogger<IEmulator> logger, IEnumerable<IStartableService> managers,
                   IEnumerable<IServer> servers, IEnumerable<IDisposableService> disposables) : IEmulator
    {
        readonly static int MAJOR = 1;
        readonly static int MINOR = 0;
        readonly static int BUILD = 0;

        public readonly static CultureInfo CultureInfo = new("it-IT");

        public readonly static DateTime UnixStart = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public readonly static string Version = $"Dolphin Emulator v{MAJOR}.{MINOR}.{BUILD}";

        public static bool Debug { get; set; } = Debugger.IsAttached;

        async Task IEmulator.Start()
        {
            Console.Title = "Dolphin || Loading";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine(@"  ██████╗  ██████╗ ██╗     ██████╗ ██╗  ██╗██╗███╗   ██╗");
            Console.WriteLine(@"  ██╔══██╗██╔═══██╗██║     ██╔══██╗██║  ██║██║████╗  ██║");
            Console.WriteLine(@"  ██║  ██║██║   ██║██║     ██████╔╝███████║██║██╔██╗ ██║");
            Console.WriteLine(@"  ██║  ██║██║   ██║██║     ██╔═══╝ ██╔══██║██║██║╚██╗██║");
            Console.WriteLine(@"  ██████╔╝╚██████╔╝███████╗██║     ██║  ██║██║██║ ╚████║");
            Console.WriteLine(@"  ╚═════╝  ╚═════╝ ╚══════╝╚═╝     ╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝");                                                                         
            Console.WriteLine();
            Console.WriteLine($"  { Version }");
            Console.WriteLine($"  Developed by RealCosis & Dario9494");
            Console.WriteLine();

            try
            {
                foreach (var manager in managers)
                    await manager.Start();

                foreach (var server in servers)
                    await server.Start();

                Console.Title = "Dolphin || Started";
            }
            catch (Exception ex)
            {
                GeneralException($"[UNHANDLED] {ex.Message}");
            }
        }

        async Task IEmulator.Dispose()
        {
            Console.Title = "Dolphin || Shutdown";

            try
            {
                foreach (var disposable in disposables)
                    await disposable.Dispose();

                foreach (var server in servers)
                    await server.Stop();

                logger.LogInformation("Dolphin has successfully shutdowned");
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                GeneralException($"[UNHANDLED] {ex.Message}");
            }
        }

        #region private methods

        private void GeneralException(string? text)
        {
            Console.Clear();

            logger.LogError("{message}", text);
            logger.LogError("Press ANY key to close the emulator.");
            Console.ReadKey();
            Environment.Exit(Environment.ExitCode);
        }

        #endregion
    }
}