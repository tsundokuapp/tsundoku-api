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
        #region => TESTES CAPITULO COMIC APP SERVICE

        public Comic GerarComic(Guid idComic)
        {
            return FixtureCustomizado.RetornaFixtureCustomizado.Build<Comic>().With(x => x.Id, idComic).Create();
        }

        public Comic GerarComic(Guid idComic, string slugObra)
        {
            return FixtureCustomizado.RetornaFixtureCustomizado.Build<Comic>().With(x => x.Id, idComic).With(x => x.Slug, slugObra).Create();
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

        #endregion

        #region => TESTES CAPITULO NOVEL APP SERVICE

        public Novel GerarNovel(Guid idNovel)
        {
            return FixtureCustomizado.RetornaFixtureCustomizado.Build<Novel>().With(x => x.Id, idNovel).Create();
        }

        public Novel GerarNovel(Guid idNovel, string slugObra)
        {
            return FixtureCustomizado.RetornaFixtureCustomizado.Build<Novel>().With(x => x.Id, idNovel).With(x => x.Slug, slugObra).Create();
        }

        public VolumeNovel GerarVolumeNovel(Guid idNovel)
        {
            return FixtureCustomizado.RetornaFixtureCustomizado.Build<VolumeNovel>().With(x => x.NovelId, idNovel).Create();
        }

        public List<CapituloNovel> GerarListaCapituloNovel(List<Guid> listaIdsCapitulos)
        {
            var listaCapituloNovel = new List<CapituloNovel>();
            listaIdsCapitulos
                .ForEach(id => listaCapituloNovel
                    .Add(FixtureCustomizado.RetornaFixtureCustomizado
                        .Build<CapituloNovel>()
                        .With(x => x.Id, id)
                        .With(x => x.ListaImagensJson, RetornaConteudoIlustracoesNovel())
                        .Create()
                    )
                );

            return listaCapituloNovel;
        }

        public static string RetornaConteudoIlustracoesNovel()
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

        #endregion
    }
}
