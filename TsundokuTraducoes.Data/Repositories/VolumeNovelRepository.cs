using Microsoft.EntityFrameworkCore;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Entities.Entities.Volume;

namespace TsundokuTraducoes.Data.Repositories
{
    public class VolumeNovelRepository(ContextBase context) : IVolumeNovelRepository
    {
        public async Task<List<VolumeNovel>> ObterVolumesNovelPorIdObra(Guid idObra)
        {
            var listaVolumes = await context.VolumesNovel
                .AsNoTracking()
                .Include(x => x.ListaCapitulo.OrderBy(x => x.OrdemCapitulo))
                .Where(x => x.NovelId == idObra)
                .OrderBy(x => x.OrdemVolume)
                .ToListAsync();

            return listaVolumes;
        }

        public async Task<List<VolumeNovel>> ObterVolumesNovelPorSlugObra(string slugObra)
        {
            var listaVolumes = await context.VolumesNovel
                .AsNoTracking()
                .Include(x => x.ListaCapitulo.OrderBy(x => x.OrdemCapitulo))
                .Where(x => x.Novel.Slug == slugObra)
                .OrderBy(x => x.OrdemVolume)
                .ToListAsync();

            return listaVolumes;
        }
    }
}
