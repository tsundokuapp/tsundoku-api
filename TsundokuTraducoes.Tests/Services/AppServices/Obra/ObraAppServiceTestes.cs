using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Services.AppServices;
using TsundokuTraducoes.Services.AppServices.Interfaces;
using TsundokuTraducoes.Services.Profiles;

namespace TsundokuTraducoes.Tests.Services.AppServices.Obra
{
    public class ObraAppServiceTestes
    {
        private readonly IMapper _mapper;
        private readonly Mock<IObrasService> _obrasServiceMock;
        private readonly Mock<IGeneroDeParaAppService> _generoDeParaAppServiceMock;

        private readonly ObrasAppService _obrasAppService;

        public ObraAppServiceTestes()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new VolumeProfile());
                cfg.AddProfile(new CapituloProfile());
                cfg.AddProfile(new ObraProfile());
                cfg.AddProfile(new GeneroProfile());
            }, NullLoggerFactory.Instance);

            _mapper = config.CreateMapper();
            _obrasServiceMock = new Mock<IObrasService>();
            _generoDeParaAppServiceMock = new Mock<IGeneroDeParaAppService>();

            _obrasAppService = new ObrasAppService(
               _obrasServiceMock.Object,
               _mapper,
               _generoDeParaAppServiceMock.Object
            );
        }

        [Fact]
        public void TrataRetornoListaNovel_ListaGeneroNovel_DeveRetornarObjetoMapeadoCorretamente()
        {
            // Arrange
            var appServicesFactory = new AppServicesFactory();
            var novel = appServicesFactory.GerarNovelComVolumeComGeneros();

            var retornoEsperado = new RetornoObras()
            {
                Titulo = "Bruxa Errante, a Jornada de Elaina",
                UrlCapa = "https://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg",
                Alias = "Bruxa Errante",
                Autor = "Shiraishi Jougi",
                DescritivoVolume = "1",
                Slug = "bruxa-errante-a-jornada-de-elaina",
                TipoObra = "Light Novel",
                Id = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                TituloAlternativo = "Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々",
                StatusObra = "Em andamento",
                ListaGeneros = ["Aventura", "Seinen", "Drama", "Fantasia"],
                Publicado = true,
                UrlCapaPrincipal = null,
                UrlCapaVolume = null
            };

            // Act
            var listaNoval = new List<Novel>
            {
                novel
            };

            var retorno = _obrasAppService.TrataListaRetornoNovel(listaNoval);
            var numeroDeCamposDaListaObra = retorno.First().GetType().GetProperties().Length;

            // Assert
            Assert.NotNull(retorno);
            Assert.Equal(retornoEsperado.Titulo, retorno.First().Titulo);
            Assert.Equal(retornoEsperado.UrlCapa, retorno.First().UrlCapa);
            Assert.Equal(retornoEsperado.Alias, retorno.First().Alias);
            Assert.Equal(retornoEsperado.Autor, retorno.First().Autor);
            Assert.Equal(retornoEsperado.DescritivoVolume, retorno.First().DescritivoVolume);
            Assert.Equal(retornoEsperado.Slug, retorno.First().Slug);
            Assert.Equal(retornoEsperado.TipoObra, retorno.First().TipoObra);
            Assert.Equal(retornoEsperado.Id, retorno.First().Id);
            Assert.Equal(retornoEsperado.TituloAlternativo, retorno.First().TituloAlternativo);
            Assert.Equal(retornoEsperado.StatusObra, retorno.First().StatusObra);
            Assert.Equal(retornoEsperado.Publicado, retorno.First().Publicado);
            Assert.Equal(retornoEsperado.ListaGeneros, retorno.First().ListaGeneros);
            Assert.Equal(retornoEsperado.UrlCapaPrincipal, retorno.First().UrlCapaPrincipal);
            Assert.Equal(retornoEsperado.UrlCapaVolume, retorno.First().UrlCapaVolume);
            Assert.Equal(14, numeroDeCamposDaListaObra);
        }

        [Fact]
        public void TrataRetornoListaComic_ListaGeneroComic_DeveRetornarObjetoMapeadoCorretamente()
        {
            // Arrange
            var appServicesFactory = new AppServicesFactory();
            var comic = appServicesFactory.GerarNovelComVolumeComGeneros();

            var retornoEsperado = new RetornoObras()
            {
                Titulo = "Bruxa Errante, a Jornada de Elaina",
                UrlCapa = "https://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg",
                Alias = "Bruxa Errante",
                Autor = "Shiraishi Jougi",
                DescritivoVolume = "1",
                Slug = "bruxa-errante-a-jornada-de-elaina",
                TipoObra = "Light Novel",
                Id = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                TituloAlternativo = "Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々",
                StatusObra = "Em andamento",
                ListaGeneros = ["Aventura", "Seinen", "Drama", "Fantasia"],
                Publicado = true,
                UrlCapaPrincipal = null,
                UrlCapaVolume = null
            };

            // Act
            var listaNoval = new List<Novel>
            {
                comic
            };

            var retorno = _obrasAppService.TrataListaRetornoNovel(listaNoval);
            var numeroDeCamposDaListaObra = retorno.First().GetType().GetProperties().Length;

            // Assert
            Assert.NotNull(retorno);
            Assert.Equal(retornoEsperado.Titulo, retorno.First().Titulo);
            Assert.Equal(retornoEsperado.UrlCapa, retorno.First().UrlCapa);
            Assert.Equal(retornoEsperado.Alias, retorno.First().Alias);
            Assert.Equal(retornoEsperado.Autor, retorno.First().Autor);
            Assert.Equal(retornoEsperado.DescritivoVolume, retorno.First().DescritivoVolume);
            Assert.Equal(retornoEsperado.Slug, retorno.First().Slug);
            Assert.Equal(retornoEsperado.TipoObra, retorno.First().TipoObra);
            Assert.Equal(retornoEsperado.Id, retorno.First().Id);
            Assert.Equal(retornoEsperado.TituloAlternativo, retorno.First().TituloAlternativo);
            Assert.Equal(retornoEsperado.StatusObra, retorno.First().StatusObra);
            Assert.Equal(retornoEsperado.Publicado, retorno.First().Publicado);
            Assert.Equal(retornoEsperado.ListaGeneros, retorno.First().ListaGeneros);
            Assert.Equal(retornoEsperado.UrlCapaPrincipal, retorno.First().UrlCapaPrincipal);
            Assert.Equal(retornoEsperado.UrlCapaVolume, retorno.First().UrlCapaVolume);
            Assert.Equal(14, numeroDeCamposDaListaObra);
        }
    }
}
