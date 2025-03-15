using AutoMapper;
using HttpContextMoq;
using HttpContextMoq.Extensions;
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
        private readonly Mock<ICapituloComicService> _capituloComicService;
        private readonly Mock<ICapituloNovelService> _capituloNovelService;
        
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
            _capituloComicService = new Mock<ICapituloComicService>();
            _capituloNovelService = new Mock<ICapituloNovelService>();

            _capituloComicAppService = new CapituloComicAppService(
               _capituloComicService.Object,
               _mapper
            );

            _capituloNovelAppService = new CapituloNovelAppService(
               _capituloNovelService.Object,
               _mapper
            );
        }

        #region => TESTES CAPITULO COMIC APP SERVICE

        [Fact]
        public void CapituloComicAppService_DeveRetornarObjetoComCapituloPorIdCapituloIdObra()
        {
            // Arrange
            var idComic = Guid.Parse("00000000-1111-2222-3333-444444444444");

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"), 
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"), 
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var capituloAppServiceFactory = new CapituloAppServiceFactory();
            Comic comic = capituloAppServiceFactory.GerarComic(idComic);
            VolumeComic volumeComic = capituloAppServiceFactory.GerarVolumeComic(idComic);
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
