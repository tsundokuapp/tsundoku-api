namespace TsundokuTraducoes.Api
{
    public static class Configuration
    {
        public static bool EhAmbienteDesenvolvimento { get; set; }
        public static string DiretorioWeb { get; set; }

        public static ConnectionStrings ConnectionString = new();

        public class ConnectionStrings
        {
            public string Default { get; set; }
        }
    }
}
