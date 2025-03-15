using AutoMapper;
using HttpContextMoq;
using HttpContextMoq.Extensions;
using Moq;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Helpers;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Domain.Services;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Services.AppServices;
using TsundokuTraducoes.Services.Profiles;

namespace TsundokuTraducoes.Tests.Services.AppServices.Capitulo
{
    public class CapituloAppServiceTestes
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICapituloComicService> _capituloComicServiceMock;
        private readonly Mock<IObraService> _ObraServiceMock;
        
        private readonly CapituloComicAppService _capituloComicAppService;
        private readonly CapituloNovelAppService _capituloNovelAppService;

        public CapituloAppServiceTestes()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new VolumeProfile());
                cfg.AddProfile(new CapituloProfile());
                cfg.AddProfile(new ObraProfile());
                cfg.AddProfile(new GeneroProfile());
            });

            _mapper = config.CreateMapper();
            _capituloComicServiceMock = new Mock<ICapituloComicService>();
            _ObraServiceMock = new Mock<IObraService>();

            _capituloComicAppService = new CapituloComicAppService(
               _capituloComicServiceMock.Object,
               _ObraServiceMock.Object,
               _mapper
            );

            _capituloNovelAppService = new CapituloNovelAppService(
               _capituloNovelService.Object,
               _mapper
            );
        }

        #region => TESTES CAPITULO COMIC APP SERVICE

        [Fact]
        public void DeveRetornarCapitulo_ComLinksParaProximoEAnterior_NaBuscaPorIdCapituloEIdbra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"), 
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"), 
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactoryTestes();
            Comic comic = capituloAppServiceFactory.GerarComic(IdObra);
            VolumeComic volumeComic = capituloAppServiceFactory.GerarVolumeComic(IdObra);
            List<CapituloComic> listaCapituloComic = capituloAppServiceFactory.GerarListaCapituloComic(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Id}/{listaIdsCapitulos[1]}";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoCapituloComic = new List<RetornoCapituloComic>();

            listaCapituloComic
                .ForEach(capituloComic => retornoListaRetornoCapituloComic
                    .Add(_capituloComicAppService
                        .TrataRetornoCapituloComic(capituloComic))
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoCapitulosComics(httpContext, retornoListaRetornoCapituloComic, listaIdsCapitulos[1]);

            Assert.True(objetoRetorno.Data.Id == listaIdsCapitulos[1]);
            Assert.Contains($"{comic.Id}/{listaIdsCapitulos[2]}", objetoRetorno.Proxima);
            Assert.Contains($"{comic.Id}/{listaIdsCapitulos[0]}", objetoRetorno.Anterior);
        }

        [Fact]
        public void DeveRetornarCapitulo_ComLinksParaProximoEAnteriorNull_NaBuscaPorIdCapituloEIdbra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactoryTestes();
            Comic comic = capituloAppServiceFactory.GerarComic(IdObra);
            VolumeComic volumeComic = capituloAppServiceFactory.GerarVolumeComic(IdObra);
            List<CapituloComic> listaCapituloComic = capituloAppServiceFactory.GerarListaCapituloComic(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Id}/{listaIdsCapitulos[0]}";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoCapituloComic = new List<RetornoCapituloComic>();

            listaCapituloComic
                .ForEach(capituloComic => retornoListaRetornoCapituloComic
                    .Add(_capituloComicAppService
                        .TrataRetornoCapituloComic(capituloComic))
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoCapitulosComics(httpContext, retornoListaRetornoCapituloComic, listaIdsCapitulos[0]);

            Assert.True(objetoRetorno.Data.Id == listaIdsCapitulos[0]);
            Assert.Contains($"{comic.Id}/{listaIdsCapitulos[1]}", objetoRetorno.Proxima);
            Assert.Null(objetoRetorno.Anterior);
        }

        [Fact]
        public void DeveRetornarCapitulo_ComLinksParaAnteriorEProximoNull_NaBuscaPorIdCapituloEIdbra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactoryTestes();
            Comic comic = capituloAppServiceFactory.GerarComic(IdObra);
            VolumeComic volumeComic = capituloAppServiceFactory.GerarVolumeComic(IdObra);
            List<CapituloComic> listaCapituloComic = capituloAppServiceFactory.GerarListaCapituloComic(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Id}/{listaIdsCapitulos[3]}";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoCapituloComic = new List<RetornoCapituloComic>();

            listaCapituloComic
                .ForEach(capituloComic => retornoListaRetornoCapituloComic
                    .Add(_capituloComicAppService
                        .TrataRetornoCapituloComic(capituloComic))
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoCapitulosComics(httpContext, retornoListaRetornoCapituloComic, listaIdsCapitulos[3]);

            Assert.True(objetoRetorno.Data.Id == listaIdsCapitulos[3]);
            Assert.Contains($"{comic.Id}/{listaIdsCapitulos[2]}", objetoRetorno.Anterior);
            Assert.Null(objetoRetorno.Proxima);
        }

        [Fact]
        public void DeveRetornarFalse_QuandoCapituloNaoEncontrado_NaBuscaPorIdCapituloEIdbra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactoryTestes();
            Comic comic = capituloAppServiceFactory.GerarComic(IdObra);
            VolumeComic volumeComic = capituloAppServiceFactory.GerarVolumeComic(IdObra);
            List<CapituloComic> listaCapituloComic = capituloAppServiceFactory.GerarListaCapituloComic(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Id}/{listaIdsCapitulos[3]}";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoCapituloComic = new List<RetornoCapituloComic>();

            listaCapituloComic
                .ForEach(capituloComic => retornoListaRetornoCapituloComic
                    .Add(_capituloComicAppService
                        .TrataRetornoCapituloComic(capituloComic))
                );

            var result = _capituloComicAppService.ValidaExisteCapituloNaLista(retornoListaRetornoCapituloComic, Guid.Parse("1000aaaa-bbbb-cccc-dddd-44444444eeee"));

            Assert.False(result.IsSuccess);
            Assert.Contains("Capítulo não encontrado!", result.Errors[0].Message);
        }

        [Fact]
        public async Task DeveRetornarNull_QuandoObraNaoEncontrada_NaBuscaPorIdCapituloEIdbra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactoryTestes();
            Comic comic = null;
            VolumeComic volumeComic = capituloAppServiceFactory.GerarVolumeComic(IdObra);
            List<CapituloComic> listaCapituloComic = capituloAppServiceFactory.GerarListaCapituloComic(listaIdsCapitulos);

            // Mock
            _ObraServiceMock.Setup(x => x.RetornaComicPorId(IdObra)).Returns(comic);

            // Assert
            var result = await _capituloComicAppService.ObterCapitulosComicPorIdObraEIdCapitulo(IdObra, listaIdsCapitulos[0]);

            Assert.False(result.IsSuccess);
            Assert.Null(result.ValueOrDefault);
            Assert.Contains("Obra não encontrada!", result.Errors[0].Message);
        }

        #endregion

        #region => TESTES CAPITULO NOVEL APP SERVICE

        [Fact]
        public void CapituloNovelAppService_DeveRetornarObjetoComCapituloPorIdCapituloIdObra()
        {
            // Arrange
            var idNovel = Guid.Parse("00000000-1111-2222-3333-444444444444");

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactory();
            Novel novel = capituloAppServiceFactory.GerarNovel(idNovel);
            VolumeNovel volumeNovel = capituloAppServiceFactory.GerarVolumeNovel(idNovel);
            List<CapituloNovel> listaCapituloNovel = capituloAppServiceFactory.GerarListaCapituloNovel(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{novel.Id}/{listaIdsCapitulos[1]}";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoCapituloNovel = new List<RetornoCapituloNovel>();

            listaCapituloNovel
                .ForEach(capituloNovel => retornoListaRetornoCapituloNovel
                    .Add(_capituloNovelAppService
                        .TrataRetornoCapituloNovel(capituloNovel))
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoCapitulosNovel(httpContext, retornoListaRetornoCapituloNovel, listaIdsCapitulos[1]);

            Assert.True(objetoRetorno.Data.Id == listaIdsCapitulos[1]);
            Assert.Contains($"{novel.Id}/{listaIdsCapitulos[2]}", objetoRetorno.Proxima);
            Assert.Contains($"{novel.Id}/{listaIdsCapitulos[0]}", objetoRetorno.Anterior);
        }

        #endregion
    }
}
