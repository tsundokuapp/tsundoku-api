using TsundokuTraducoes.Api.Services.Interfaces;

namespace TsundokuTraducoes.Api.Services
{
    public class ValidacaoTratamentoObrasService : IValidacaoTratamentoObrasService
    {
        public bool ValidaParametrosNovel(string pesquisar, string nacionalidade, string status, string tipo, string genero, int? skip, int? take)
        {
            return ValidaParametrosObra(pesquisar, nacionalidade, status, tipo, genero, skip, take);
        }

        public bool ValidaParametrosObra(string pesquisar, string nacionalidade, string status, string tipo, string genero, int? skip, int? take)
        {
            return !string.IsNullOrEmpty(pesquisar) ||
                   !string.IsNullOrEmpty(nacionalidade) ||
                   !string.IsNullOrEmpty(status) ||
                   !string.IsNullOrEmpty(tipo) ||
                   !string.IsNullOrEmpty(genero) ||
                   skip != null ||
                   take != null;
        }

        public int RetornaSkipTratado(int? pagina)
        {
            return pagina == null ? 0 : pagina.GetValueOrDefault();
        }

        public int RetornaTakeTratado(int? obrasPorPagina)
        {
            return obrasPorPagina == null ? 4 : obrasPorPagina.GetValueOrDefault();
        }
    }
}
