namespace Dolphin.Configurations
{
    public class DatabaseConfiguration
    {
        public string? Host { get; set; }

        public int Port { get; set; }

        public string? User { get; set; }

        public string? Password { get; set; }

        public string? Name { get; set; }

        public int PoolMinSize { get; set; }

        public int PoolMaxSize { get; set; }
    }
}