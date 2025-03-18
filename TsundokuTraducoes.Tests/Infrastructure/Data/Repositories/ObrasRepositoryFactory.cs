using AutoFixture;
using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Tests.Utils;

namespace TsundokuTraducoes.Tests.Infrastructure.Data.Repositories
{
    public class ObrasRepositoryFactory()
    {
        public Genero GerarGenero(string genero)
        {
            return FixtureCustomizado.RetornaFixtureCustomizado.Build<Genero>().With(x => x.Descricao, genero).Create();
        }
    }
}
