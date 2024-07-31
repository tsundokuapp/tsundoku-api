﻿namespace TsundokuTraducoes.Helpers
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

            var diretorioCriado = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "assets", "images", Path.Combine(paths));            
            CriaDiretorio(diretorioCriado);
            return diretorioCriado;
        }

        public static bool ExcluirDiretorioLocal(string diretorioImagens)
        {
            bool diretorioExcluido;

            try
            {
                if (Directory.Exists(diretorioImagens))
                {
                    Directory.Delete(diretorioImagens, true);
                }

                diretorioExcluido = true;
            }
            catch (Exception)
            {
                diretorioExcluido = false;
            }

            return diretorioExcluido;
        }
    }    
}