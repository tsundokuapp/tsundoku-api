using Microsoft.Extensions.Configuration;

namespace TsundokuTraducoes.Api.Utilidades
{
    public static class Constantes
    {
        public static string Hifen = "-";
        public static string Underline = "_";
        public static string UrlDiretioWebImagens = @"https://api-tsun-teste-assinatura-01.azurewebsites.net/";
        public static string UrlDiretorioLocalHostImagens = @"https:\\localhost:5001\";
        public static string StringDeConexao = RetornaStringDeConexao();
        public static string AmbienteDesenvolvimento = RetornaValorAmbienteDesenvolvimento();

        private static string RetornaStringDeConexao()
        {
            return new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("Default");
        }

        private static string RetornaValorAmbienteDesenvolvimento()
        {
            return new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
                .GetValue<string>("AmbienteDesenvolvimento:Valor");
        }
    }
}
