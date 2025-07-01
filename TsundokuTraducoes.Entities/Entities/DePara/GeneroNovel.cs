using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Entities.Entities.Obra;

namespace TsundokuTraducoes.Entities.Entities.DePara
{
    public class GeneroNovel
    {
        public Guid NovelId { get; set; }
        public Novel Novel { get; set; }
        public Guid GeneroId { get; set; }
        public Genero Genero { get; set; }

        public GeneroNovel()
        {
                
        }      

        public void AdicionaGeneroNovel(Guid novelId, Guid generoId)
        {
            NovelId = novelId;
            GeneroId = generoId;
        }
    }
}
