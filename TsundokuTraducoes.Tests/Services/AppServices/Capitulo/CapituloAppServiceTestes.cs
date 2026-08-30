using AutoMapper;
using HttpContextMoq;
using HttpContextMoq.Extensions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using TsundokuTraducoes.Api.Helpers;
using TsundokuTraducoes.Domain.Interfaces.Services;
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
        private readonly Mock<ICapituloNovelService> _capituloNovelService;
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
            }, NullLoggerFactory.Instance);

            _mapper = config.CreateMapper();
            _capituloComicServiceMock = new Mock<ICapituloComicService>();
            _capituloNovelService = new Mock<ICapituloNovelService>();
            _ObraServiceMock = new Mock<IObraService>();

            _capituloComicAppService = new CapituloComicAppService(
               _capituloComicServiceMock.Object,
               _ObraServiceMock.Object,
               _mapper
            );

            _capituloNovelAppService = new CapituloNovelAppService(
               _capituloNovelService.Object,
               _ObraServiceMock.Object,
               _mapper
            );
        }

        #region => TESTES CAPITULO COMIC APP SERVICE

        [Fact(DisplayName = nameof(DeveRetornarCapitulo_ComLinksParaProximoEAnterior_NaBuscaPorIdCapituloEIdbra))]
        [Trait("Services", "AppService - CapituloComics")]
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

        [Fact(DisplayName = nameof(DeveRetornarCapitulo_ComLinksParaProximoEAnteriorNull_NaBuscaPorIdCapituloEIdbra))]
        [Trait("Services", "AppService - CapituloComics")]
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

        [Fact(DisplayName = nameof(DeveRetornarCapitulo_ComLinksParaAnteriorEProximoNull_NaBuscaPorIdCapituloEIdbra))]
        [Trait("Services", "AppService - CapituloComics")]
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

        [Fact(DisplayName = nameof(DeveRetornarFalse_QuandoCapituloNaoEncontrado_NaBuscaPorIdCapituloEIdbra))]
        [Trait("Services", "AppService - CapituloComics")]
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

        [Fact(DisplayName = nameof(DeveRetornarNull_QuandoObraNaoEncontrada_NaBuscaPorIdCapituloEIdbra))]
        [Trait("Services", "AppService - CapituloComics")]
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


        [Fact(DisplayName = nameof(DeveRetornarCapitulo_ComLinksParaProximoEAnterior_NaBuscaPorIdCapituloESlugObra))]
        [Trait("Services", "AppService - CapituloComics")]
        public void DeveRetornarCapitulo_ComLinksParaProximoEAnterior_NaBuscaPorIdCapituloESlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-comic-teste";

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactoryTestes();
            Comic comic = capituloAppServiceFactory.GerarComic(IdObra, slugObra);
            VolumeComic volumeComic = capituloAppServiceFactory.GerarVolumeComic(IdObra);
            List<CapituloComic> listaCapituloComic = capituloAppServiceFactory.GerarListaCapituloComic(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Slug}/{listaIdsCapitulos[1]}";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);
            
            // Assert
            var retornoListaRetornoCapituloComic = new List<RetornoCapituloComic>();

            listaCapituloComic
                .ForEach(capituloComic => retornoListaRetornoCapituloComic
                    .Add(_capituloComicAppService
                        .TrataRetornoCapituloComic(capituloComic))
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoCapitulosPorSlugComics(httpContext, retornoListaRetornoCapituloComic, listaIdsCapitulos[1]);

            Assert.Equal(slugObra, comic.Slug);
            Assert.True(objetoRetorno.Data.Id == listaIdsCapitulos[1]);
            Assert.Contains($"{comic.Slug}/{listaIdsCapitulos[2]}", objetoRetorno.Proxima);
            Assert.Contains($"{comic.Slug}/{listaIdsCapitulos[0]}", objetoRetorno.Anterior);
        }

        [Fact(DisplayName = nameof(DeveRetornarCapitulo_ComLinksParaProximoEAnteriorNull_NaBuscaPorIdCapituloESlugObra))]
        [Trait("Services", "AppService - CapituloComics")]
        public void DeveRetornarCapitulo_ComLinksParaProximoEAnteriorNull_NaBuscaPorIdCapituloESlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-comic-teste";

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactoryTestes();
            Comic comic = capituloAppServiceFactory.GerarComic(IdObra, slugObra);
            VolumeComic volumeComic = capituloAppServiceFactory.GerarVolumeComic(IdObra);
            List<CapituloComic> listaCapituloComic = capituloAppServiceFactory.GerarListaCapituloComic(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Slug}/{listaIdsCapitulos[0]}";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoCapituloComic = new List<RetornoCapituloComic>();

            listaCapituloComic
                .ForEach(capituloComic => retornoListaRetornoCapituloComic
                    .Add(_capituloComicAppService
                        .TrataRetornoCapituloComic(capituloComic))
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoCapitulosPorSlugComics(httpContext, retornoListaRetornoCapituloComic, listaIdsCapitulos[0]);

            Assert.Equal(slugObra, comic.Slug);
            Assert.True(objetoRetorno.Data.Id == listaIdsCapitulos[0]);
            Assert.Contains($"{comic.Slug}/{listaIdsCapitulos[1]}", objetoRetorno.Proxima);
            Assert.Null(objetoRetorno.Anterior);
        }

        [Fact(DisplayName = nameof(DeveRetornarCapitulo_ComLinksParaAnteriorEProximoNull_NaBuscaPorIdCapituloESlugObra))]
        [Trait("Services", "AppService - CapituloComics")]
        public void DeveRetornarCapitulo_ComLinksParaAnteriorEProximoNull_NaBuscaPorIdCapituloESlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-comic-teste";

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactoryTestes();
            Comic comic = capituloAppServiceFactory.GerarComic(IdObra, slugObra);
            VolumeComic volumeComic = capituloAppServiceFactory.GerarVolumeComic(IdObra);
            List<CapituloComic> listaCapituloComic = capituloAppServiceFactory.GerarListaCapituloComic(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Slug}/{listaIdsCapitulos[3]}";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoCapituloComic = new List<RetornoCapituloComic>();

            listaCapituloComic
                .ForEach(capituloComic => retornoListaRetornoCapituloComic
                    .Add(_capituloComicAppService
                        .TrataRetornoCapituloComic(capituloComic))
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoCapitulosPorSlugComics(httpContext, retornoListaRetornoCapituloComic, listaIdsCapitulos[3]);

            Assert.Equal(slugObra, comic.Slug);
            Assert.True(objetoRetorno.Data.Id == listaIdsCapitulos[3]);
            Assert.Contains($"{comic.Slug}/{listaIdsCapitulos[2]}", objetoRetorno.Anterior);
            Assert.Null(objetoRetorno.Proxima);
        }

        [Fact(DisplayName = nameof(DeveRetornarFalse_QuandoCapituloNaoEncontrado_NaBuscaPorIdCapituloESlugObra))]
        [Trait("Services", "AppService - CapituloComics")]
        public void DeveRetornarFalse_QuandoCapituloNaoEncontrado_NaBuscaPorIdCapituloESlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-comic-teste";

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactoryTestes();
            Comic comic = capituloAppServiceFactory.GerarComic(IdObra, slugObra);
            VolumeComic volumeComic = capituloAppServiceFactory.GerarVolumeComic(IdObra);
            List<CapituloComic> listaCapituloComic = capituloAppServiceFactory.GerarListaCapituloComic(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Slug}/{listaIdsCapitulos[3]}";
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

            Assert.Equal(slugObra, comic.Slug);
            Assert.False(result.IsSuccess);
            Assert.Contains("Capítulo não encontrado!", result.Errors[0].Message);
        }

        [Fact(DisplayName = nameof(DeveRetornarNull_QuandoObraNaoEncontrada_NaBuscaPorIdCapituloESlugObra))]
        [Trait("Services", "AppService - CapituloComics")]
        public async Task DeveRetornarNull_QuandoObraNaoEncontrada_NaBuscaPorIdCapituloESlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-comic-teste";

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
            _ObraServiceMock.Setup(x => x.RetornaComicPorSlug(slugObra)).Returns(comic);

            // Assert
            var result = await _capituloComicAppService.ObterCapitulosComicPorIdObraEIdCapitulo(IdObra, listaIdsCapitulos[0]);
                        
            Assert.False(result.IsSuccess);
            Assert.Null(result.ValueOrDefault);
            Assert.Contains("Obra não encontrada!", result.Errors[0].Message);
        }


        [Fact(DisplayName = nameof(DeveRetornarListaCapitulos_ComLinksParaProximoEAnterior_NaBuscaPorSlugObra))]
        [Trait("Services", "AppService - CapituloComics")]
        public void DeveRetornarListaCapitulos_ComLinksParaProximoEAnterior_NaBuscaPorSlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-comic-teste";

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
                Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
                Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
                Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023"),
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
                Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
                Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
                Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactoryTestes();
            Comic comic = capituloAppServiceFactory.GerarComic(IdObra, slugObra);
            VolumeComic volumeComic = capituloAppServiceFactory.GerarVolumeComic(IdObra);
            List<CapituloComic> listaCapituloComic = capituloAppServiceFactory.GerarListaCapituloComic(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Slug}?skip=1";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoCapituloComic = new List<RetornoCapituloComic>();

            listaCapituloComic
                .ForEach(capituloComic => retornoListaRetornoCapituloComic
                    .Add(_capituloComicAppService
                        .TrataRetornoCapituloComic(capituloComic))
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetornoCapitulosPorSlugObra(httpContext, retornoListaRetornoCapituloComic, 1, null);

            Assert.Equal(15, objetoRetorno.Data.Count);
            Assert.Equal(16, objetoRetorno.Total);
            Assert.Contains($"{comic.Slug}?Skip=16&Take=15", objetoRetorno.Proxima);
            Assert.Contains($"{comic.Slug}?Skip=0&Take=15", objetoRetorno.Anterior);
        }

        [Fact(DisplayName = nameof(DeveRetornarListaCapitulos_ComLinksParaProximoEAnterior_NaBuscaPorSlugObra))]
        [Trait("Services", "AppService - CapituloComics")]
        public void DeveRetornarListaCapitulos_ComLinksParaProximoEAnteriorNull_NaBuscaPorSlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-comic-teste";

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
                Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
                Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
                Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023"),
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
                Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
                Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
                Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactoryTestes();
            Comic comic = capituloAppServiceFactory.GerarComic(IdObra, slugObra);
            VolumeComic volumeComic = capituloAppServiceFactory.GerarVolumeComic(IdObra);
            List<CapituloComic> listaCapituloComic = capituloAppServiceFactory.GerarListaCapituloComic(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Slug}?skip=0&take=15";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoCapituloComic = new List<RetornoCapituloComic>();

            listaCapituloComic
                .ForEach(capituloComic => retornoListaRetornoCapituloComic
                    .Add(_capituloComicAppService
                        .TrataRetornoCapituloComic(capituloComic))
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetornoCapitulosPorSlugObra(httpContext, retornoListaRetornoCapituloComic, 0, 15);

            Assert.Equal(slugObra, comic.Slug);
            Assert.Equal(15, objetoRetorno.Data.Count);
            Assert.Equal(16, objetoRetorno.Total);
            Assert.Contains($"{comic.Slug}?Skip=15&Take=15", objetoRetorno.Proxima);
            Assert.Null(objetoRetorno.Anterior);
        }

        [Fact(DisplayName = nameof(DeveRetornarListaCapitulos_ComLinksParaProximoEAnterior_NaBuscaPorSlugObra))]
        [Trait("Services", "AppService - CapituloComics")]
        public void DeveRetornarListaCapitulos_ComLinksParaAnteriorEProximoNull_NaBuscaPorSlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-comic-teste";

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
                Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
                Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
                Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023"),
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
                Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
                Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
                Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactoryTestes();
            Comic comic = capituloAppServiceFactory.GerarComic(IdObra, slugObra);
            VolumeComic volumeComic = capituloAppServiceFactory.GerarVolumeComic(IdObra);
            List<CapituloComic> listaCapituloComic = capituloAppServiceFactory.GerarListaCapituloComic(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Slug}?skip=15&take=15";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoCapituloComic = new List<RetornoCapituloComic>();

            listaCapituloComic
                .ForEach(capituloComic => retornoListaRetornoCapituloComic
                    .Add(_capituloComicAppService
                        .TrataRetornoCapituloComic(capituloComic))
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetornoCapitulosPorSlugObra(httpContext, retornoListaRetornoCapituloComic, 15, 15);

            Assert.Equal(slugObra, comic.Slug);
            Assert.Single(objetoRetorno.Data);
            Assert.Equal(16, objetoRetorno.Total);
            Assert.Contains($"{comic.Slug}?Skip=0&Take=15", objetoRetorno.Anterior);
            Assert.Null(objetoRetorno.Proxima);
        }

        #endregion

        #region => TESTES CAPITULO NOVEL APP SERVICE

        [Fact(DisplayName = nameof(DeveRetornarCapituloNovel_ComLinksParaProximoEAnterior_NaBuscaPorIdCapituloEIdbra))]
        [Trait("Services", "AppService - CapituloNovels")]
        public void DeveRetornarCapituloNovel_ComLinksParaProximoEAnterior_NaBuscaPorIdCapituloEIdbra()
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

            var capituloAppServiceFactory = new CapituloAppServiceFactoryTestes();
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

        [Fact(DisplayName = nameof(DeveRetornarCapituloNovel_ComLinksParaProximoEAnteriorNull_NaBuscaPorIdCapituloEIdbra))]
        [Trait("Services", "AppService - CapituloNovels")]
        public void DeveRetornarCapituloNovel_ComLinksParaProximoEAnteriorNull_NaBuscaPorIdCapituloEIdbra()
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
            Novel novel = capituloAppServiceFactory.GerarNovel(IdObra);
            VolumeNovel volumeNovel = capituloAppServiceFactory.GerarVolumeNovel(IdObra);
            List<CapituloNovel> listaCapituloNovel = capituloAppServiceFactory.GerarListaCapituloNovel(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{novel.Id}/{listaIdsCapitulos[0]}";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoCapituloNovel = new List<RetornoCapituloNovel>();

            listaCapituloNovel
                .ForEach(capituloNovel => retornoListaRetornoCapituloNovel
                    .Add(_capituloNovelAppService
                        .TrataRetornoCapituloNovel(capituloNovel))
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoCapitulosNovel(httpContext, retornoListaRetornoCapituloNovel, listaIdsCapitulos[0]);

            Assert.True(objetoRetorno.Data.Id == listaIdsCapitulos[0]);
            Assert.Contains($"{novel.Id}/{listaIdsCapitulos[1]}", objetoRetorno.Proxima);
            Assert.Null(objetoRetorno.Anterior);
        }

        [Fact(DisplayName = nameof(DeveRetornarCapituloNovel_ComLinksParaAnteriorEProximoNull_NaBuscaPorIdCapituloEIdbra))]
        [Trait("Services", "AppService - CapituloNovels")]
        public void DeveRetornarCapituloNovel_ComLinksParaAnteriorEProximoNull_NaBuscaPorIdCapituloEIdbra()
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
            Novel novel = capituloAppServiceFactory.GerarNovel(IdObra);
            VolumeNovel volumeNovel = capituloAppServiceFactory.GerarVolumeNovel(IdObra);
            List<CapituloNovel> listaCapituloNovel = capituloAppServiceFactory.GerarListaCapituloNovel(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{novel.Id}/{listaIdsCapitulos[3]}";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoCapituloNovel = new List<RetornoCapituloNovel>();

            listaCapituloNovel
                .ForEach(capituloNovel => retornoListaRetornoCapituloNovel
                    .Add(_capituloNovelAppService
                        .TrataRetornoCapituloNovel(capituloNovel))
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoCapitulosNovel(httpContext, retornoListaRetornoCapituloNovel, listaIdsCapitulos[3]);

            Assert.True(objetoRetorno.Data.Id == listaIdsCapitulos[3]);
            Assert.Contains($"{novel.Id}/{listaIdsCapitulos[2]}", objetoRetorno.Anterior);
            Assert.Null(objetoRetorno.Proxima);
        }

        [Fact(DisplayName = nameof(DeveRetornarFalseCapituloNovel_QuandoCapituloNaoEncontrado_NaBuscaPorIdCapituloEIdbra))]
        [Trait("Services", "AppService - CapituloNovels")]
        public void DeveRetornarFalseCapituloNovel_QuandoCapituloNaoEncontrado_NaBuscaPorIdCapituloEIdbra()
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
            Novel novel = capituloAppServiceFactory.GerarNovel(IdObra);
            VolumeNovel volumeNovel = capituloAppServiceFactory.GerarVolumeNovel(IdObra);
            List<CapituloNovel> listaCapituloNovel = capituloAppServiceFactory.GerarListaCapituloNovel(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{novel.Id}/{listaIdsCapitulos[3]}";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoCapituloNovel = new List<RetornoCapituloNovel>();

            listaCapituloNovel
                .ForEach(capituloNovel => retornoListaRetornoCapituloNovel
                    .Add(_capituloNovelAppService
                        .TrataRetornoCapituloNovel(capituloNovel))
                );

            var result = _capituloNovelAppService.ValidaExisteCapituloNaLista(retornoListaRetornoCapituloNovel, Guid.Parse("1000aaaa-bbbb-cccc-dddd-44444444eeee"));

            Assert.False(result.IsSuccess);
            Assert.Contains("Capítulo não encontrado!", result.Errors[0].Message);
        }

        [Fact(DisplayName = nameof(DeveRetornarNullCapituloNovel_QuandoObraNaoEncontrada_NaBuscaPorIdCapituloEIdbra))]
        [Trait("Services", "AppService - CapituloNovels")]
        public async Task DeveRetornarNullCapituloNovel_QuandoObraNaoEncontrada_NaBuscaPorIdCapituloEIdbra()
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
            Novel novel = null;
            VolumeNovel volumeNovel = capituloAppServiceFactory.GerarVolumeNovel(IdObra);
            List<CapituloNovel> listaCapituloNovel = capituloAppServiceFactory.GerarListaCapituloNovel(listaIdsCapitulos);

            // Mock
            _ObraServiceMock.Setup(x => x.RetornaNovelPorId(IdObra)).Returns(novel);

            // Assert
            var result = await _capituloNovelAppService.ObterCapitulosNovelPorIdObraEIdCapitulo(IdObra, listaIdsCapitulos[0]);

            Assert.False(result.IsSuccess);
            Assert.Null(result.ValueOrDefault);
            Assert.Contains("Obra não encontrada!", result.Errors[0].Message);
        }

        [Fact(DisplayName = nameof(DeveRetornarCapituloNovel_ComLinksParaProximoEAnterior_NaBuscaPorIdCapituloESlugObra))]
        [Trait("Services", "AppService - CapituloNovels")]
        public void DeveRetornarCapituloNovel_ComLinksParaProximoEAnterior_NaBuscaPorIdCapituloESlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-novel-teste";           

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactoryTestes();
            Novel novel = capituloAppServiceFactory.GerarNovel(IdObra, slugObra);
            VolumeNovel volumeNovel = capituloAppServiceFactory.GerarVolumeNovel(IdObra);
            List<CapituloNovel> listaCapituloNovel = capituloAppServiceFactory.GerarListaCapituloNovel(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{novel.Slug}/{listaIdsCapitulos[1]}";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoCapituloNovel = new List<RetornoCapituloNovel>();

            listaCapituloNovel
                .ForEach(capituloNovel => retornoListaRetornoCapituloNovel
                    .Add(_capituloNovelAppService
                        .TrataRetornoCapituloNovel(capituloNovel))
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoCapitulosPorSlugNovels(httpContext, retornoListaRetornoCapituloNovel, listaIdsCapitulos[1]);

            Assert.True(objetoRetorno.Data.Id == listaIdsCapitulos[1]);
            Assert.Contains($"{novel.Slug}/{listaIdsCapitulos[2]}", objetoRetorno.Proxima);
            Assert.Contains($"{novel.Slug}/{listaIdsCapitulos[0]}", objetoRetorno.Anterior);
        }

        [Fact(DisplayName = nameof(DeveRetornarCapituloNovel_ComLinksParaProximoEAnteriorNull_NaBuscaPorIdCapituloESlugObra))]
        [Trait("Services", "AppService - CapituloNovels")]
        public void DeveRetornarCapituloNovel_ComLinksParaProximoEAnteriorNull_NaBuscaPorIdCapituloESlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-novel-teste";

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactoryTestes();
            Novel novel = capituloAppServiceFactory.GerarNovel(IdObra, slugObra);
            VolumeNovel volumeNovel = capituloAppServiceFactory.GerarVolumeNovel(IdObra);
            List<CapituloNovel> listaCapituloNovel = capituloAppServiceFactory.GerarListaCapituloNovel(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{novel.Slug}/{listaIdsCapitulos[0]}";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoCapituloNovel = new List<RetornoCapituloNovel>();

            listaCapituloNovel
                .ForEach(capituloNovel => retornoListaRetornoCapituloNovel
                    .Add(_capituloNovelAppService
                        .TrataRetornoCapituloNovel(capituloNovel))
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoCapitulosPorSlugNovels(httpContext, retornoListaRetornoCapituloNovel, listaIdsCapitulos[0]);

            Assert.True(objetoRetorno.Data.Id == listaIdsCapitulos[0]);
            Assert.Contains($"{novel.Slug}/{listaIdsCapitulos[1]}", objetoRetorno.Proxima);
            Assert.Null(objetoRetorno.Anterior);
        }

        [Fact(DisplayName = nameof(DeveRetornarCapituloNovel_ComLinksParaAnteriorEProximoNull_NaBuscaPorIdCapituloESlugObra))]
        [Trait("Services", "AppService - CapituloNovels")]
        public void DeveRetornarCapituloNovel_ComLinksParaAnteriorEProximoNull_NaBuscaPorIdCapituloESlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-novel-teste";

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactoryTestes();
            Novel novel = capituloAppServiceFactory.GerarNovel(IdObra, slugObra);
            VolumeNovel volumeNovel = capituloAppServiceFactory.GerarVolumeNovel(IdObra);
            List<CapituloNovel> listaCapituloNovel = capituloAppServiceFactory.GerarListaCapituloNovel(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{novel.Slug}/{listaIdsCapitulos[3]}";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoCapituloNovel = new List<RetornoCapituloNovel>();

            listaCapituloNovel
                .ForEach(capituloNovel => retornoListaRetornoCapituloNovel
                    .Add(_capituloNovelAppService
                        .TrataRetornoCapituloNovel(capituloNovel))
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoCapitulosPorSlugNovels(httpContext, retornoListaRetornoCapituloNovel, listaIdsCapitulos[3]);

            Assert.True(objetoRetorno.Data.Id == listaIdsCapitulos[3]);
            Assert.Contains($"{novel.Slug}/{listaIdsCapitulos[2]}", objetoRetorno.Anterior);
            Assert.Null(objetoRetorno.Proxima);
        }

        [Fact(DisplayName = nameof(DeveRetornarFalseCapituloNovel_QuandoCapituloNaoEncontrado_NaBuscaPorIdCapituloESlugObra))]
        [Trait("Services", "AppService - CapituloNovels")]
        public void DeveRetornarFalseCapituloNovel_QuandoCapituloNaoEncontrado_NaBuscaPorIdCapituloESlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-novel-teste";

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactoryTestes();
            Novel novel = capituloAppServiceFactory.GerarNovel(IdObra, slugObra);
            VolumeNovel volumeNovel = capituloAppServiceFactory.GerarVolumeNovel(IdObra);
            List<CapituloNovel> listaCapituloNovel = capituloAppServiceFactory.GerarListaCapituloNovel(listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{novel.Slug}/{listaIdsCapitulos[3]}";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoCapituloNovel = new List<RetornoCapituloNovel>();

            listaCapituloNovel
                .ForEach(capituloNovel => retornoListaRetornoCapituloNovel
                    .Add(_capituloNovelAppService
                        .TrataRetornoCapituloNovel(capituloNovel))
                );

            var result = _capituloNovelAppService.ValidaExisteCapituloNaLista(retornoListaRetornoCapituloNovel, Guid.Parse("1000aaaa-bbbb-cccc-dddd-44444444eeee"));

            Assert.False(result.IsSuccess);
            Assert.Contains("Capítulo não encontrado!", result.Errors[0].Message);
        }

        [Fact(DisplayName = nameof(DeveRetornarNullCapituloNovel_QuandoObraNaoEncontrada_NaBuscaPorIdCapituloESlugObra))]
        [Trait("Services", "AppService - CapituloNovels")]
        public async Task DeveRetornarNullCapituloNovel_QuandoObraNaoEncontrada_NaBuscaPorIdCapituloESlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-novel-teste";

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactoryTestes();
            Novel novel = null;
            VolumeNovel volumeNovel = capituloAppServiceFactory.GerarVolumeNovel(IdObra);
            List<CapituloNovel> listaCapituloNovel = capituloAppServiceFactory.GerarListaCapituloNovel(listaIdsCapitulos);

            // Mock
            _ObraServiceMock.Setup(x => x.RetornaNovelPorSlug(slugObra)).Returns(novel);

            // Assert
            var result = await _capituloNovelAppService.ObterCapitulosNovelPorIdObraEIdCapitulo(IdObra, listaIdsCapitulos[0]);

            Assert.False(result.IsSuccess);
            Assert.Null(result.ValueOrDefault);
            Assert.Contains("Obra não encontrada!", result.Errors[0].Message);
        }

        #endregion
    }
}