using AutoMapper;
using Moq;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Services.AppServices.Interfaces;
using TsundokuTraducoes.Services.Profiles;

namespace TsundokuTraducoes.Services.AppServices
{
    public class ObraAppServiceTestes
    {
        private readonly IMapper _mapper;
        private readonly Mock<IObrasService> _obrasServiceMock;
        private readonly Mock<IImagemAppService> _imagemAppServiceMock;
        private readonly Mock<IGeneroDeParaAppService> _generoDeParaAppServiceMock;

        private readonly ObraAppService _obraAppService;

        public ObraAppServiceTestes()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new VolumeProfile());
                cfg.AddProfile(new CapituloProfile());
                cfg.AddProfile(new ObraProfile());
                cfg.AddProfile(new GeneroProfile());
            });

            _mapper = config.CreateMapper();
            _obrasServiceMock = new Mock<IObrasService>();
            _generoDeParaAppServiceMock = new Mock<IGeneroDeParaAppService>();

            //_obraAppService = new ObrasAppService(
            //   _obrasServiceMock.Object,
            //   _mapper,
            //   _generoDeParaAppServiceMock.Object
            //);
        }

        [Fact]
        public void TrataRetornoListaNovel_ListaGeneroNovel_DeveRetornarObjetoMapeadoCorretamente()
        {
            // Arrange
            var appServicesFactory = new AppServicesFactory();
            var novel = appServicesFactory.GerarNovel();

            //var retornoEsperado = new RetornoVolume()
            //{
            //    DataInclusao = "01/01/2024 10:30:00",
            //    DataAlteracao = "02/01/2024 14:45:00",
            //    UsuarioAlteracao = "UsuarioTeste",
            //    UsuarioInclusao = "UsuarioTeste",
            //    ListaCapitulo = new List<RetornoCapituloNoVolume>
            //    {
            //        new RetornoCapituloNoVolume()
            //        {
            //            Id = idCapituloCriado,
            //            VolumeId = idVolumeCriado,
            //            Titulo = "Capítulo 1",
            //            Numero = "1",
            //            Parte = "1",
            //            OrdemCapitulo = 1,
            //            Slug = "capitulo-1",
            //            DataInclusao = new DateTime(2024, 1, 1, 10, 30, 0),
            //            Publicado = true,
            //        }
            //    }
            //};

            var retornoEsperado = new RetornoObras()
            {
                Titulo = "Bruxa Errante, a Jornada de Elaina",
                UrlCapa = "https://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V8_Capa.jpg",
                Alias = "Bruxa Errante",
                Autor = "Shiraishi Jougi",
                DescritivoVolume = "Volume 01",
                Slug = "bruxa-errante-a-jornada-de-elaina",
                TipoObra = "Light Novel",
                Id = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                TituloAlternativo = "Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々",
                StatusObra = "Em andamento",
                ListaGeneros = ["Aventura", "Seinen", "Drama", "Fantasia"],
                Publicado = true,
            };

            // Act
            //var retorno = _obraAppService;
            //var numeroDeCamposDaListaCapitulo = retorno.ListaCapitulo.First().GetType().GetProperties().Length;

            // Assert
            //Assert.NotNull(retorno);
            //Assert.Equal(retornoEsperado.DataInclusao, retorno.DataInclusao);
            //Assert.Equal(retornoEsperado.DataAlteracao, retorno.DataAlteracao);
            //Assert.Equal(retornoEsperado.UsuarioAlteracao, retorno.UsuarioAlteracao);
            //Assert.Equal(retornoEsperado.ListaCapitulo.Count, retorno.ListaCapitulo.Count);
            //Assert.Equal(9, numeroDeCamposDaListaCapitulo);

            //Assert.Equal(retornoEsperado.ListaCapitulo.First().Id, retorno.ListaCapitulo.First().Id);
            //Assert.Equal(retornoEsperado.ListaCapitulo.First().VolumeId, retorno.ListaCapitulo.First().VolumeId);
            //Assert.Equal(retornoEsperado.ListaCapitulo.First().Titulo, retorno.ListaCapitulo.First().Titulo);
            //Assert.Equal(retornoEsperado.ListaCapitulo.First().Numero, retorno.ListaCapitulo.First().Numero);
            //Assert.Equal(retornoEsperado.ListaCapitulo.First().Parte, retorno.ListaCapitulo.First().Parte);
            //Assert.Equal(retornoEsperado.ListaCapitulo.First().OrdemCapitulo, retorno.ListaCapitulo.First().OrdemCapitulo);
            //Assert.Equal(retornoEsperado.ListaCapitulo.First().Slug, retorno.ListaCapitulo.First().Slug);
            //Assert.Equal(retornoEsperado.ListaCapitulo.First().DataInclusao, retorno.ListaCapitulo.First().DataInclusao);
            //Assert.Equal(retornoEsperado.ListaCapitulo.First().Publicado, retorno.ListaCapitulo.First().Publicado);
        }
    }
}
