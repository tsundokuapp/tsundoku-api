using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Entities.Entities.Obra;

namespace TsundokuTraducoes.Tests.Infrastructure.Data.Repositories.Generos
{
    public class GeneroRepositoryFactory
    {
        #region => MOCK OBRAS

        public Novel GerarNovel()
        {
            return new Novel() { Id = Guid.NewGuid() };
        }

        public Comic GerarComic()
        {
            return new Comic() { Id = Guid.NewGuid() };
        }

        #endregion
                
        #region => MOCK GENERO
        
        public Genero GerarGenero(string descricao, string slug)
        {
            return new Genero { Id = Guid.NewGuid(), Descricao = descricao, Slug = slug, DataInclusao = DateTime.Now, DataAlteracao = DateTime.Now };
        }

        #endregion

        #region => MOCK GENERO NOVEL
        
        public GeneroNovel GerarGeneroNovel(Guid idObra, Guid idGenero)
        {
            return new GeneroNovel() { NovelId = idObra, GeneroId = idGenero };
        }        

        #endregion

        #region => MOCK GENERO COMIC
        
        public GeneroComic GerarGeneroComic(Guid idObra, Guid idGenero)
        {
            return new GeneroComic() { ComicId = idObra, GeneroId = idGenero };
        }
        
        #endregion
    }
}