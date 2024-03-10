using TsundokuTraducoes.Helpers.DTOs.Public.Request;

namespace TsundokuTraducoes.Helpers.Validacao
{
    public static class ValidacaoRequest
    {
        public static bool ValidaParametrosNovel(RequestObras requestObras)
        {
            return ValidaParametrosObra(requestObras);
        }

        public static bool ValidaParametrosObra(RequestObras requestObras)
        {
            return !string.IsNullOrEmpty(requestObras.Pesquisar) ||
                   !string.IsNullOrEmpty(requestObras.Nacionalidade) ||
                   !string.IsNullOrEmpty(requestObras.Status) ||
                   !string.IsNullOrEmpty(requestObras.Tipo) ||
                   !string.IsNullOrEmpty(requestObras.Genero) ||
                   requestObras.Skip != null ||
                   requestObras.Take != null;
        }

        public static int RetornaSkipTratado(int? pagina)
        {
            return pagina == null ? 0 : pagina.GetValueOrDefault();
        }

        public static int RetornaTakeTratado(int? obrasPorPagina, bool home = false)
        {
            var valorObrasPorPagina = home == true ? 5 : 4;
            return obrasPorPagina == null ? valorObrasPorPagina : obrasPorPagina.GetValueOrDefault();
        }
    }
}
