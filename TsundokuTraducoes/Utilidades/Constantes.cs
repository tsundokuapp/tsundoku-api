using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

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

            //var stringBuilder = new MySqlConnectionStringBuilder();
            //stringBuilder.Server = "aws.connect.psdb.cloud";
            //stringBuilder.UserID = "0mqdp47t1zvbclrqd2ar";
            //stringBuilder.Password = "pscale_pw_HBdUbdWGnSATpMnuqxXUMI4I6U30hTJU7ETVMnae8zN";
            //stringBuilder.Port = 3306;
            //stringBuilder.Database = "tsun-db";
            //stringBuilder.SslMode = MySqlSslMode.VerifyFull;
            //stringBuilder.CertificateFile = "./Authentication/cacert_2023_01_10.pem";
            //stringBuilder.CertificatePassword = "12345678";
            //stringBuilder.CertificateStoreLocation = MySqlCertificateStoreLocation.CurrentUser;


            //var retorno = stringBuilder.ToString();
            //return retorno;


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
