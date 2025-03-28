using FluentResults;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Services.AppServices.Interfaces
{
    public interface IVolumeNovelAppService
    {
        Task<Result<List<RetornoVolumeNovel>>> ObterVolumesNovelPorIdObra(Guid idObra);
        Task<Result<List<RetornoVolumeNovel>>> ObterVolumesNovelPorSlugObra(string slugObra);
    }
}
