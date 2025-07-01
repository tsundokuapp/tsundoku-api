using FluentResults;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Services.AppServices.Interfaces
{
    public interface IVolumeComicAppService
    {
        Task<Result<List<RetornoVolumeComic>>> ObterVolumesComicPorIdObra(Guid idObra);
        Task<Result<List<RetornoVolumeComic>>> ObterVolumesComicPorSlugObra(string slugObra);
    }
}
