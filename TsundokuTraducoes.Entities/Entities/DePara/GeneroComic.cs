using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Entities.Entities.Obra;

namespace TsundokuTraducoes.Entities.Entities.DePara
{
    public class GeneroComic
    {
        public Comic Comic { get; set; }
        public Guid ComicId { get; set; }
        public Genero Genero { get; set; }
        public Guid GeneroId { get; set; }

        public GeneroComic() { }

        public void AdicionaGeneroComic(Guid comicId, Guid generoId)
        {
            ComicId = comicId;
            GeneroId = generoId;
        }
    }
}
