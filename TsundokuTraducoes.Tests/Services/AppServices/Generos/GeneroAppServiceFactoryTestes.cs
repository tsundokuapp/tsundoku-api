using AutoFixture;
using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Tests.Utils;

namespace TsundokuTraducoes.Tests.Services.AppServices.Generos
{
    public class GeneroAppServiceFactoryTestes
    {
        public Genero GerarGenero(string descricaoGenero, string slugGenero)
        {
            return FixtureCustomizado.RetornaFixtureCustomizado
                .Build<Genero>()
                .With(x => x.Descricao, descricaoGenero)
                .With(x => x.Slug, slugGenero)
                .Create();
        }

        public List<Genero> GerarListaGeneros(Dictionary<string, string> dicionarioDescricaoGenero)
        {
            var listaGenero = new List<Genero>();

            foreach (var genero in dicionarioDescricaoGenero)
            {
                listaGenero.Add(GerarGenero(genero.Key, genero.Value));
            }

            return listaGenero;
        }
    }
}
