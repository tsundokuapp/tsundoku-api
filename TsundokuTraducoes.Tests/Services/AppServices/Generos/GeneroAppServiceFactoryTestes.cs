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

        public Genero GerarGeneroComTodosDados(string descricaoGenero, string slugGenero)
        {
            return FixtureCustomizado.RetornaFixtureCustomizado
                .Build<Genero>()
                .With(x => x.Descricao, descricaoGenero)
                .With(x => x.Slug, slugGenero)
                .With(x => x.UsuarioInclusao, "UsuarioTeste")
                .With(x => x.UsuarioAlteracao, "UsuarioTeste")
                .With(x => x.DataInclusao, DateTime.Now)
                .With(x => x.DataAlteracao, DateTime.Now)
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

        public List<Genero> GerarListaGenerosComTodosDados(Dictionary<string, string> dicionarioDescricaoGenero)
        {
            var listaGenero = new List<Genero>();

            foreach (var genero in dicionarioDescricaoGenero)
            {
                listaGenero.Add(GerarGeneroComTodosDados(genero.Key, genero.Value));
            }

            return listaGenero;
        }
    }
}
