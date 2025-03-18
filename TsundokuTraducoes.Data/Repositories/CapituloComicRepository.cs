using Microsoft.EntityFrameworkCore;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Entities.Entities.Capitulo;

namespace TsundokuTraducoes.Data.Repositories
{
    public class CapituloComicRepository(ContextBase context) : ICapituloComicRepository
    {
        public async Task<List<CapituloComic>> ObterCapitulosComicPorIdObra(Guid idObra)
        {

            var query = from capitulosComic in context.CapitulosComic.AsNoTracking()
                        join volumesComic in context.VolumesComic.AsNoTracking()
                          on capitulosComic.VolumeId equals volumesComic.Id
                        join comics in context.Comics.AsNoTracking()
                          on volumesComic.ComicId equals comics.Id
                       where comics.Id == idObra
                     orderby capitulosComic.OrdemCapitulo
                      select capitulosComic;
            
            return await query.ToListAsync();
        }
    }
}
