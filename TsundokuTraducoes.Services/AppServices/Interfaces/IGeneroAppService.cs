using FluentResults;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Services.AppServices.Interfaces
{
    public interface IGeneroAppService
    {
        Task<Result<List<RetornoGenero>>> RetornaListaGeneros();
        Task<Result<RetornoGenero>> RetornaGeneroPorId(Guid id);
        Task<Result<RetornoGenero>> AdicionaGenero(GeneroDTO generoDTO);
        Task<Result<RetornoGenero>> AtualizaGenero(GeneroDTO generoDTO);
        Task<Result<bool>> ExcluiGenero(Guid id);
    }
}
