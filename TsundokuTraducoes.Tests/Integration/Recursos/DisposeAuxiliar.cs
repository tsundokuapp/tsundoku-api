using TsundokuTraducoes.Helpers;

namespace TsundokuTraducoes.Integration.Tests.Recursos
{
    public static class DisposeAuxiliar
    {
        public static bool Dispose(string diretorioImagens)
        {
            return Diretorios.ExcluirDiretorioLocal(diretorioImagens);
        }
    }
}
