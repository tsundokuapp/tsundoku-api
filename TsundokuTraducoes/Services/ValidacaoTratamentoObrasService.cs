using TsundokuTraducoes.Api.DTOs.Admin.Request;
using TsundokuTraducoes.Api.Services.Interfaces;

namespace TsundokuTraducoes.Api.Services
{
    public class ValidacaoTratamentoObrasService : IValidacaoTratamentoObrasService
    {
        public bool ValidaParametrosNovel(RequestObras requestObras)
        {
            return ValidaParametrosObra(requestObras);
        }

        public bool ValidaParametrosObra(RequestObras requestObras)
        {
            return !string.IsNullOrEmpty(requestObras.Pesquisar) ||
                   !string.IsNullOrEmpty(requestObras.Nacionalidade) ||
                   !string.IsNullOrEmpty(requestObras.Status) ||
                   !string.IsNullOrEmpty(requestObras.Tipo) ||
                   !string.IsNullOrEmpty(requestObras.Genero) ||
                   requestObras.Skip != null ||
                   requestObras.Take != null;
        }

        public int RetornaSkipTratado(int? pagina)
        {
            return pagina == null ? 0 : pagina.GetValueOrDefault();
        }

        public int RetornaTakeTratado(int? obrasPorPagina, bool home = false)
        {
            var valorObrasPorPagina = home == true ? 5 : 4;
            return obrasPorPagina == null ? valorObrasPorPagina : obrasPorPagina.GetValueOrDefault();
        }
    }
}
