using AutoMapper;
using HttpContextMoq;
using HttpContextMoq.Extensions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using TsundokuTraducoes.Api.Helpers;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Services.AppServices;
using TsundokuTraducoes.Services.AppServices.Interfaces;
using TsundokuTraducoes.Services.Profiles;

namespace TsundokuTraducoes.Tests.Services.AppServices
{
    public class ObrasAppServiceTestes
    {
        private readonly IMapper _mapper;
        private readonly Mock<IObrasService> _obrasServiceMock;
        private readonly Mock<IGeneroDeParaAppService> _generoDeParaAppServiceMock;

        private readonly ObrasAppService _obrasAppServiceMock;

        public ObrasAppServiceTestes()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ObrasProfile());
            }, NullLoggerFactory.Instance);

            _mapper = config.CreateMapper();
            _obrasServiceMock = new Mock<IObrasService>();
            _generoDeParaAppServiceMock = new Mock<IGeneroDeParaAppService>();

            _obrasAppServiceMock = new ObrasAppService(
                _obrasServiceMock.Object,
                _mapper,
                _generoDeParaAppServiceMock.Object
            );
        }

        [Fact]
        public void TrataRetornoObrasRecomendadas_ListaObrasRecomendadas_DeveRetonarQuantidadeCorretaObrasRecomendadas()
        {
            // Arrange
            var appServicesFactory = new AppServicesFactory();
            var listaComicsRecomendadas = appServicesFactory.GerarListaComicsRecomendadas(3);
            var listaNovelsRecomendadas = appServicesFactory.GerarListaNovelsRecomendadas(4);

            var retornoEsperado = new List<RetornoObrasRecomendadas>();

            for (int i = 0; i < 3; i++)
            {
                retornoEsperado.Add(
                    new RetornoObrasRecomendadas
                    {
                        Titulo = $"Hatsukoi Losstime_{i}{i}",
                        Sinopse = "Em um mundo onde apenas duas pessoas se moviam...",
                        TipoObra = "Mangá",
                        SlugObra = "hatsukoi-losstime",
                        Capa = "https://tsundoku.com.br/wp-content/uploads/2022/01/cover_hatsukoi_vol2.jpg",
                    }
                );
            }

            for (int i = 0; i < 3; i++)
            {
                retornoEsperado.Add(
                    new RetornoObrasRecomendadas
                    {
                        Titulo = $"Bruxa Errante_{i}{i}",
                        Sinopse = "A Bruxa, Sim, sou eu.",
                        TipoObra = "Light Novel",
                        SlugObra = "bruxa-errante-a-jornada-de-elaina",
                        Capa = "https://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V8_Capa.jpg",
                    }
                );
            }

            // Act
            var retorno = _obrasAppServiceMock.TratamentoRetornoObrasRecomendadas(listaNovelsRecomendadas, listaComicsRecomendadas);
            var numeroDeCamposDaListaObrasRecomendas = retorno.First().GetType().GetProperties().Length;           

            // Assert
            Assert.True(retorno.Any());
            Assert.Equal(6, retorno.Count);            
            Assert.Equal(5, numeroDeCamposDaListaObrasRecomendas);
          
            foreach (var esperado in retornoEsperado)
            {
                var encontrado = retorno.FirstOrDefault(ret => ret.Titulo == esperado.Titulo);
                Assert.NotNull(encontrado);
                Assert.Equal(esperado.Sinopse, encontrado.Sinopse);
                Assert.Equal(esperado.TipoObra, encontrado.TipoObra);
                Assert.Equal(esperado.SlugObra, encontrado.SlugObra);
                Assert.Equal(esperado.Capa, encontrado.Capa);
            }
        }

        [Fact]
        public void RetornaNovel_ListaGeneros_DeveRetornarListaGeneros()
        {
            // Arrange
            var appServicesFactory = new AppServicesFactory();
            var banner = "https://tsundoku.com.br/wp-content/uploads/2023/12/O-comeco-depois-dofim.jpg";
            var novel = appServicesFactory.GerarNovelComBanner(banner);

            var generos = appServicesFactory.GerarListaGenerosPorParametros(["Aventura", "Fantasia"]);
            var generoNovel = appServicesFactory.GerarNovel_Com_ListaGeneroNovels(novel, generos);
            
            // Act
            List<RetornoNovel> listaRetornoNovel = new List<RetornoNovel>();
            List<Novel> novels = [novel];            


            foreach (var item in novels)
            {
                var retornoNovel = _obrasAppServiceMock.TrataRetornoNovelUnica(item);
                listaRetornoNovel.Add(retornoNovel);
            }

            // Assert
            Assert.True(listaRetornoNovel.Any());

            Assert.Equal(banner, listaRetornoNovel[0].UrlBanner);
            Assert.Equal("Aventura", listaRetornoNovel[0].ListaGeneros[0]);
            Assert.Equal("Fantasia", listaRetornoNovel[0].ListaGeneros[1]);

        }

        [Fact]
        public void RetornaComic_ListaGeneros_DeveRetornarListaGeneros()
        {
            // Arrange
            var appServicesFactory = new AppServicesFactory();
            var banner = "https://tsundoku.com.br/wp-content/uploads/2022/01/hatsukoBanner.jpg";
            var comic = appServicesFactory.GerarComicComBanner(banner);

            var generos = appServicesFactory.GerarListaGenerosPorParametros(["Aventura", "Fantasia"]);
            var generoComic = appServicesFactory.GerarComic_Com_ListaGeneroComics(comic, generos);

            // Act
            List<RetornoComic> listaRetornoComic = new List<RetornoComic>();
            List<Comic> novels = [comic];


            foreach (var item in novels)
            {
                var retornoComic = _obrasAppServiceMock.TrataRetornoComicUnica(item);
                listaRetornoComic.Add(retornoComic);
            }

            // Assert
            Assert.True(listaRetornoComic.Any());

            Assert.Equal(banner, listaRetornoComic[0].UrlBanner);
            Assert.Equal("Aventura", listaRetornoComic[0].ListaGeneros[0]);
            Assert.Equal("Fantasia", listaRetornoComic[0].ListaGeneros[1]);
        }

        [Fact]
        public void RetornaNovelsRecentes_DeveRetornarListaNovelsRecentes()
        {
            // Arrange
            var dicionarioObrasRecentes = new Dictionary<string, DateTime>
            {
                { "Obra_1", new DateTime(2025, 04, 05, 00, 00, 01) },
                { "Obra_4", new DateTime(2025, 04, 05, 00, 00, 03) },
                { "Obra_5", new DateTime(2025, 04, 05, 00, 00, 07) },
                { "Obra_2", new DateTime(2025, 04, 05, 00, 00, 10) },
                { "Obra_3", new DateTime(2025, 04, 05, 00, 00, 15) },
                { "Obra_6", new DateTime(2025, 04, 05, 00, 00, 30) }
            };

            var appServicesFactory = new AppServicesFactory();
            var listaNovelsRecentes = appServicesFactory.GerarListaNovelsRecentes(dicionarioObrasRecentes);

            var scheme = "https";
            var host = "localhost";
            var path = $"/api/obras/novels/recentes";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Mock
            var retornoNovelsRecentes = new List<RetornoNovelsRecentes>();

            listaNovelsRecentes
                .ForEach(novel => retornoNovelsRecentes.Add(
                        _obrasAppServiceMock.TrataRetornoNovelRecentes(novel)
                    )
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoNovelsRecentes(httpContext, retornoNovelsRecentes, null, null);

            var numeroDeCamposDaListaObrasRecomendas = objetoRetorno.Data.First().GetType().GetProperties().Length;

            // Assert
            Assert.Equal(13, numeroDeCamposDaListaObrasRecomendas);
            Assert.Equal("Obra_6", objetoRetorno.Data[0].Titulo);
            Assert.Equal("Obra_2", objetoRetorno.Data[2].Titulo);
            Assert.Equal("Obra_4", objetoRetorno.Data[4].Titulo);
        }

        [Fact]
        public void RetornaComicsRecentes_DeveRetornarListaComicsRecentes()
        {
            // Arrange
            var dicionarioObrasRecentes = new Dictionary<string, DateTime>
            {
                { "Obra_1", new DateTime(2025, 04, 05, 00, 00, 01) },
                { "Obra_4", new DateTime(2025, 04, 05, 00, 00, 03) },
                { "Obra_5", new DateTime(2025, 04, 05, 00, 00, 07) },
                { "Obra_2", new DateTime(2025, 04, 05, 00, 00, 10) },
                { "Obra_3", new DateTime(2025, 04, 05, 00, 00, 15) },
                { "Obra_6", new DateTime(2025, 04, 05, 00, 00, 30) }
            };

            var appServicesFactory = new AppServicesFactory();
            var listaComicsRecentes = appServicesFactory.GerarListaComicsRecentes(dicionarioObrasRecentes);

            var scheme = "https";
            var host = "localhost";
            var path = $"/api/obras/comics/recentes";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Mock
            var retornoComicsRecentes = new List<RetornoComicsRecentes>();

            listaComicsRecentes
                .ForEach(comic => retornoComicsRecentes.Add(
                        _obrasAppServiceMock.TrataRetornoComicRecentes(comic)
                    )
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoComicsRecentes(httpContext, retornoComicsRecentes, null, null);

            var numeroDeCamposDaListaObrasRecomendas = objetoRetorno.Data.First().GetType().GetProperties().Length;

            // Assert
            Assert.Equal(13, numeroDeCamposDaListaObrasRecomendas);
            Assert.Equal("Obra_6", objetoRetorno.Data[0].Titulo);
            Assert.Equal("Obra_2", objetoRetorno.Data[2].Titulo);
            Assert.Equal("Obra_4", objetoRetorno.Data[4].Titulo);
        }
    }
}