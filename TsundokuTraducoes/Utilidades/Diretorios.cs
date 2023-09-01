using System.IO;

namespace TsundokuTraducoes.Api.Utilidades
{
    public static class Diretorios
    {
        public static void CriaDiretorio(string diretorio)
        {
            // TODO - Verificar comportamento em outros sistemas (SO)
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }
        }

        public static string RetornaDiretorioImagemCriado(params string[] paths)
        {
            if (string.IsNullOrEmpty(Configuration.DiretorioWeb))
            {
                Configuration.DiretorioWeb = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            var diretorioCriado = Path.Combine(Configuration.DiretorioWeb, "assets", "images", Path.Combine(paths));            
            CriaDiretorio(diretorioCriado);
            return diretorioCriado;
        }
    }    
}
