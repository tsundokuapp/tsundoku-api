using Microsoft.EntityFrameworkCore;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Entities.Entities.Volume;

namespace TsundokuTraducoes.Data.Repositories
{
    public class VolumeComicRepository(ContextBase context) : IVolumeComicRepository
    {
        public async Task<List<VolumeComic>> ObterVolumesComicPorIdObra(Guid idObra)
        {
            var listaVolumes = await context.VolumesComic
                .AsNoTracking()
                .Include(x => x.ListaCapitulo.OrderBy(x => x.OrdemCapitulo))
                .Where(x => x.ComicId == idObra)
                .OrderBy(x => x.OrdemVolume)
                .ToListAsync();

            return listaVolumes;
        }

        public async Task<List<VolumeComic>> ObterVolumesComicPorSlugObra(string slugObra)
        {
            var listaVolumes = await context.VolumesComic
                .AsNoTracking()
                .Include(x => x.ListaCapitulo.OrderBy(x => x.OrdemCapitulo))
                .Where(x => x.Comic.Slug == slugObra)
                .OrderBy(x => x.OrdemVolume)
                .ToListAsync();

            return listaVolumes;
        }
    }
}
