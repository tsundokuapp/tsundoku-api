using TsundokuTraducoes.Api.DTOs.Admin.Request;

namespace TsundokuTraducoes.Api.Services.Interfaces
{
    public interface IValidacaoTratamentoObrasService
    {
        bool ValidaParametrosObra(RequestObras requestObras);
        bool ValidaParametrosNovel(RequestObras requestObras);
        int RetornaSkipTratado(int? pagina);
        int RetornaTakeTratado(int? pagina, bool home = false);
    }
}
