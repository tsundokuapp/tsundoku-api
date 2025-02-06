using AutoMapper;
using Moq;
using TsundokuTraducoes.Domain.Interfaces.Services;
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
            });

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
            Assert.Empty(retornoEsperado.Intersect(retorno).ToList());
            Assert.Equal(6, retorno.Where(ret => retornoEsperado.Where(retExp => retExp.Titulo == ret.Titulo).Any()).ToList().Count);
            Assert.Equal(6, retorno.Where(ret => retornoEsperado.Where(retExp => retExp.Sinopse == ret.Sinopse).Any()).ToList().Count);
            Assert.Equal(6, retorno.Where(ret => retornoEsperado.Where(retExp => retExp.TipoObra == ret.TipoObra).Any()).ToList().Count);
            Assert.Equal(6, retorno.Where(ret => retornoEsperado.Where(retExp => retExp.SlugObra == ret.SlugObra).Any()).ToList().Count);
            Assert.Equal(6, retorno.Where(ret => retornoEsperado.Where(retExp => retExp.Capa == ret.Capa).Any()).ToList().Count);
        }
    }
}