using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.Obra;

namespace TsundokuTraducoes.Data.Repositories
{
    public class CapituloRepository : ICapituloRepository
    {
        public Task AdicionaCapituloComic(CapituloComic volumeComic)
        {
            throw new NotImplementedException();
        }

        public Task AdicionaCapituloNovel(CapituloNovel volumeNovel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AlteracoesSalvas()
        {
            throw new NotImplementedException();
        }

        public Task<CapituloComic> AtualizaCapituloComic(CapituloComic volumeComic)
        {
            throw new NotImplementedException();
        }

        public Task<CapituloNovel> AtualizaCapituloNovel(CapituloNovel volumeNovel)
        {
            throw new NotImplementedException();
        }

        public void AtualizaComicPorCapitulo(Comic comic, CapituloComic capituloComic)
        {
            throw new NotImplementedException();
        }

        public void AtualizaNovelPorCapitulo(Novel novel, CapituloNovel capituloNovel)
        {
            throw new NotImplementedException();
        }

        public void ExcluiCapituloComic(CapituloComic volumeComic)
        {
            throw new NotImplementedException();
        }

        public void ExcluiCapituloNovel(CapituloNovel volumeNovel)
        {
            throw new NotImplementedException();
        }

        public Task<CapituloComic> RetornaCapituloComicExistente(CapituloComic capituloComic)
        {
            throw new NotImplementedException();
        }

        public Task<CapituloComic> RetornaCapituloComicPorId(Guid capituloId)
        {
            throw new NotImplementedException();
        }

        public Task<CapituloNovel> RetornaCapituloNovelExistente(CapituloNovel capituloNovel)
        {
            throw new NotImplementedException();
        }

        public Task<CapituloNovel> RetornaCapituloNovelPorId(Guid capituloId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CapituloComic>> RetornaListaCapitulosComic(Guid? volumeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CapituloNovel>> RetornaListaCapitulosNovel(Guid? volumeId)
        {
            throw new NotImplementedException();
        }
    }
}
