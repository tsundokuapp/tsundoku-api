using TsundokuTraducoes.Entities.Entities.Capitulo;

namespace TsundokuTraducoes.Domain.Interfaces.Services
{
    public interface ICapituloNovelService
    {
        Task<List<CapituloNovel>> ObterCapitulosNovelPorIdObra(Guid idObra);
        Task<List<CapituloNovel>> ObterCapitulosNovelPorSlugObra(string slugObra);
    }
}
