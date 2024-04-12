namespace TsundokuTraducoes.Helpers.Configuration
{
    public static class ConfigurationExternal
    {
        private static AcessoExterno _acessoExterno;

        public static void SetaAcessoExterno(AcessoExterno acessoExterno)
        {
            _acessoExterno = acessoExterno;
        }

        public static string RetornaApiKeyTinify()
        {
            return _acessoExterno.ApiKeyTinify;
        }
    }

    public class AcessoExterno
    {
        public string ApiKeyTinify { get; set; }
    }
}
