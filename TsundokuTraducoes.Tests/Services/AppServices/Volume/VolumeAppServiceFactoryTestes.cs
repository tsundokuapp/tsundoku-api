using AutoFixture;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Tests.Utils;

namespace TsundokuTraducoes.Tests.Services.AppServices.Volume
{
    public class VolumeAppServiceFactoryTestes
    {
        #region => TESTES VOLUME COMIC APP SERVICE

        public Comic GerarComic(Guid idComic)
        {
            return FixtureCustomizado.RetornaFixtureCustomizado.Build<Comic>().With(x => x.Id, idComic).Create();
        }

        public Comic GerarComic(Guid idComic, string slugObra)
        {
            return FixtureCustomizado.RetornaFixtureCustomizado.Build<Comic>().With(x => x.Id, idComic).With(x => x.Slug, slugObra).Create();
        }

        public List<VolumeComic> GerarListaVolumeComic(Guid idComic, List<Guid> listaIdsCapitulos)
        {
            var listaCapituloComic = new List<VolumeComic>();
            listaIdsCapitulos
                .ForEach(id => listaCapituloComic
                    .Add(FixtureCustomizado.RetornaFixtureCustomizado
                        .Build<VolumeComic>()
                        .With(x => x.Id, id)
                        .With(x => x.ComicId, idComic)
                        .Create()
                    )
                );

            return listaCapituloComic;
        }

        #endregion
    }
}
