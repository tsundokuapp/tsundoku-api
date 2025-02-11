using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Entities.Entities.Generos;

namespace TsundokuTraducoes.Tests.Entities.Generos
{
    public class GenerosFactory
    {
        public Genero GerarGenero()
        {
            var genero = new Genero();
            genero.AdicionaGenero(Guid.NewGuid(), "Horror", "horror", "Usuário Teste", "Usuário Teste", DateTime.Now, DateTime.Now);

            return genero;
        }

        public List<Genero> GerarListaGeneros()
        {
            var listaGeneros = new List<Genero>();
            var dicionarioGeneros = new Dictionary<string, string>
            {
                { "Aventura", "aventura" },
                { "Seinen", "seinen" },
                { "Drama", "drama" },
                { "Fantasia", "fantasia" },
            };

            foreach (var item in dicionarioGeneros)
            {
                var genero = new Genero();
                genero.AdicionaGenero(Guid.NewGuid(), item.Key, item.Value, "Usuário Teste", "Usuário Teste", DateTime.Now, DateTime.Now);
                listaGeneros.Add(genero);
            }

            return listaGeneros;
        }

        public GeneroNovel GerarGeneroNovel() 
        {
            var generoNovel = new GeneroNovel();
            generoNovel.AdicionaGeneroNovel(Guid.NewGuid(), Guid.NewGuid());
            return generoNovel;
        }

        public GeneroComic GerarGeneroComic()
        {
            var generoComic = new GeneroComic();
            generoComic.AdicionaGeneroComic(Guid.NewGuid(), Guid.NewGuid());
            return generoComic;
        }
    }
}