using Microsoft.EntityFrameworkCore;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Entities.Entities.Capitulo;

namespace TsundokuTraducoes.Data.Repositories
{
    public class CapituloNovelRepository(ContextBase context) : ICapituloNovelRepository
    {
        public async Task<List<CapituloNovel>> ObterCapitulosNovelPorIdObra(Guid idObra)
        {
            var query = from capitulosNovel in context.CapitulosNovel.AsNoTracking()
                        join volumesNovel in context.VolumesNovel.AsNoTracking()
                          on capitulosNovel.VolumeId equals volumesNovel.Id
                        join novels in context.Novels.AsNoTracking()
                          on volumesNovel.NovelId equals novels.Id
                        where novels.Id == idObra
                        orderby volumesNovel.OrdemVolume, capitulosNovel.OrdemCapitulo
                        select capitulosNovel;

            return await query.ToListAsync();
        }

        public async Task<List<CapituloNovel>> ObterCapitulosNovelPorIdSlug(string slugObra)
        {
            var query = from capitulosNovel in context.CapitulosNovel.AsNoTracking()
                        join volumesNovel in context.VolumesNovel.AsNoTracking()
                          on capitulosNovel.VolumeId equals volumesNovel.Id
                        join novels in context.Novels.AsNoTracking()
                          on volumesNovel.NovelId equals novels.Id
                        where novels.Slug == slugObra
                        orderby volumesNovel.OrdemVolume, capitulosNovel.OrdemCapitulo
                        select capitulosNovel;

            return await query.ToListAsync();
        }
    }
}
