using System.IO;

namespace TsundokuTraducoes.Api.Utilidades
{
    public static class Diretorios
    {
        public static void CriaDiretorio(string diretorio)
        {
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }
        }
    }    
}
