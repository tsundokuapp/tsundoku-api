using TsundokuTraducoes.Entities.Entities.Capitulo;

namespace TsundokuTraducoes.Domain.Interfaces.Repositories
{
    public interface ICapituloNovelRepository
    {
        Task<List<CapituloNovel>> ObterCapitulosNovelPorIdObra(Guid idObra);
    }
}
