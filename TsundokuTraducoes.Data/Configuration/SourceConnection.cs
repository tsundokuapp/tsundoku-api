namespace TsundokuTraducoes.Data.Configuration
{
    public static class SourceConnection
    {
        private static ConnectionStringConfig _connectionStringConfig;

        public static void SetaConnectionStringConfig(ConnectionStringConfig connectionStringConfig)
        {
            _connectionStringConfig = connectionStringConfig;
        }

        public static string RetornaConnectionStringConfig()
        {
            return _connectionStringConfig.ConnectionString;
        }
    }
}
