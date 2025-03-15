using AutoFixture;
using Newtonsoft.Json;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Tests.Utils;

namespace TsundokuTraducoes.Tests.Services.AppServices.Capitulo
{
    public class CapituloAppServiceFactoryTestes
    {
        public Comic GerarComic(Guid idComic)
        {
            return FixtureCustomizado.RetornaFixtureCustomizado.Build<Comic>().With(x => x.Id, idComic).Create();
        }

        public VolumeComic GerarVolumeComic(Guid idComic)
        {
            return FixtureCustomizado.RetornaFixtureCustomizado.Build<VolumeComic>().With(x => x.ComicId, idComic).Create();
        }

        public List<CapituloComic> GerarListaCapituloComic(List<Guid> listaIdsCapitulos)
        {
            var listaCapituloComic = new List<CapituloComic>();
            listaIdsCapitulos
                .ForEach(id => listaCapituloComic
                    .Add(FixtureCustomizado.RetornaFixtureCustomizado
                        .Build<CapituloComic>()
                        .With(x => x.Id, id)
                        .With(x => x.ListaImagensJson, RetornaConteudoComic())
                        .Create()
                    )
                );

            return listaCapituloComic;
        }

        public static string RetornaConteudoComic()
        {
            var lista = new List<EnderecoImagemDTO>
            {
               new EnderecoImagemDTO {
                   Id=1,
                   Ordem=1,
                   Url="http://tsundoku.com.br/wp-content/uploads/2022/01/0-46.jpg"
               },
               new EnderecoImagemDTO {
                  Id=2,
                  Ordem=2,
                  Url="http://tsundoku.com.br/wp-content/uploads/2022/01/0-47.jpg"
               },
               new EnderecoImagemDTO {
                  Id=3,
                  Ordem=3,
                  Url="http://tsundoku.com.br/wp-content/uploads/2022/01/1-60.jpg"
               }
            };

            return JsonConvert.SerializeObject(lista);
        }
    }
}
