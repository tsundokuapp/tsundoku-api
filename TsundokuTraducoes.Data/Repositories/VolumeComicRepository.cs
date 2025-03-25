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
            var query = from volumesComic in context.VolumesComic.AsNoTracking()                          
                        join comics in context.Comics.AsNoTracking()
                          on volumesComic.ComicId equals comics.Id
                        where comics.Id == idObra
                        orderby volumesComic.Numero
                        select volumesComic;

            return await query.ToListAsync();
        }

        public async Task<List<VolumeComic>> ObterVolumesComicPorSlugObra(string slugObra)
        {
            var query = from volumesComic in context.VolumesComic.AsNoTracking()
                        join comics in context.Comics.AsNoTracking()
                          on volumesComic.ComicId equals comics.Id
                        where comics.Slug == slugObra
                        orderby volumesComic.Numero
                        select volumesComic;

            return await query.ToListAsync();
        }
    }
}
