using AutoMapper;
using HttpContextMoq;
using HttpContextMoq.Extensions;
using Moq;
using TsundokuTraducoes.Api.Helpers;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Services.AppServices;
using TsundokuTraducoes.Services.Profiles;

namespace TsundokuTraducoes.Tests.Services.AppServices.Generos
{
    public class GeneroAppServiceTestes
    {
        private readonly IMapper _mapper;
        private readonly Mock<IGeneroService> _generoServiceMock;
        private readonly GeneroAppService _generoAppService;

        public GeneroAppServiceTestes()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new VolumeProfile());
                cfg.AddProfile(new CapituloProfile());
                cfg.AddProfile(new ObraProfile());
                cfg.AddProfile(new GeneroProfile());
            });

            _mapper = config.CreateMapper();
            _generoServiceMock = new Mock<IGeneroService>();

            _generoAppService = new GeneroAppService(
                _generoServiceMock.Object,
                _mapper
            );
        }

        //[Fact]
        //public async Task DeveRetornarListaGeneros_ComLinksParaProximoEAnterior()
        //{
        //    // Arrange
        //    var dicionarioGeneros = new Dictionary<string, string>()
        //    {
        //        { "Ação","acao" },
        //        { "Aventura","aventura" },
        //        { "Fantasia","fantasia" },
        //        { "Comédia","comedia" },
        //        { "Drama","drama" },
        //        { "Harém","harem" },
        //        { "Horror","horror" }
        //    };

        //    var generoAppServiceFactory = new GeneroAppServiceFactoryTestes();
        //    var listaGeneros = generoAppServiceFactory.GerarListaGeneros(dicionarioGeneros);

        //    var scheme = "https";
        //    var host = "localhost";
        //    var path = $"/mock/generos?skip=1";
        //    var url = $"{scheme}://{host}/{path}";
        //    var httpContext = new HttpContextMock().SetupUrl(url);

        //    _generoServiceMock
        //        .Setup(x => x.RetornaListaGenerosCadastrados())
        //        .ReturnsAsync(listaGeneros);

        //    var listaGenerosCadastrados = await _generoAppService.RetornaListaGenerosCadastrados();
        //    var objetoRetorno = RequestHelper.CriarObjetoRetornoGenerosCadastrados(httpContext, listaGenerosCadastrados.ValueOrDefault, 1, null);

        //    // Assert
        //    Assert.Equal(6, objetoRetorno.Data.Count);
        //    Assert.Contains($"generos?Skip=7&Take=6", objetoRetorno.Proxima);
        //    Assert.Contains($"generos?Skip=0&Take=6", objetoRetorno.Anterior);
        //}

        [Fact]
        public async Task DeveRetornarListaGeneros_ComLinksParaProximoEAnteriorNull()
        {
            // Arrange
            var dicionarioGeneros = new Dictionary<string, string>()
            {
                { "Ação","acao" },
                { "Aventura","aventura" },
                { "Fantasia","fantasia" },
                { "Comédia","comedia" },
                { "Drama","drama" },
                { "Harém","harem" },
                { "Horror","horror" }
            };

            var generoAppServiceFactory = new GeneroAppServiceFactoryTestes();
            var listaGeneros = generoAppServiceFactory.GerarListaGeneros(dicionarioGeneros);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/generos?skip=0&take=6";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            _generoServiceMock
                .Setup(x => x.RetornaListaGenerosCadastrados())
                .ReturnsAsync(listaGeneros);

            var listaGenerosCadastrados = await _generoAppService.RetornaListaGenerosCadastrados();
            var objetoRetorno = RequestHelper.CriarObjetoRetornoGenerosCadastrados(httpContext, listaGenerosCadastrados.ValueOrDefault, 0, 6);

            // Assert
            Assert.Equal(6, objetoRetorno.Data.Count);
            Assert.Contains($"generos?Skip=6&Take=6", objetoRetorno.Proxima);
            Assert.Null(objetoRetorno.Anterior);
        }

        [Fact]
        public async Task DeveRetornarListaGeneros_ComLinksParaAnteriorEProximoNull()
        {
            // Arrange
            var dicionarioGeneros = new Dictionary<string, string>()
            {
                { "Ação","acao" },
                { "Aventura","aventura" },
                { "Fantasia","fantasia" },
                { "Comédia","comedia" },
                { "Drama","drama" },
                { "Harém","harem" },
                { "Horror","horror" }
            };

            var generoAppServiceFactory = new GeneroAppServiceFactoryTestes();
            var listaGeneros = generoAppServiceFactory.GerarListaGeneros(dicionarioGeneros);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/generos?skip=6&take=6";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            var retornoListaRetornoGeneroCadastrado = new List<RetornoGeneroCadastrado>();

            _generoServiceMock
                .Setup(x => x.RetornaListaGenerosCadastrados())
                .ReturnsAsync(listaGeneros);

            var listaGenerosCadastrados = await _generoAppService.RetornaListaGenerosCadastrados();
            var objetoRetorno = RequestHelper.CriarObjetoRetornoGenerosCadastrados(httpContext, listaGenerosCadastrados.ValueOrDefault, 6, 6);

            // Assert
            Assert.Single(objetoRetorno.Data);
            Assert.Contains($"generos?Skip=0&Take=6", objetoRetorno.Anterior);
            Assert.Null(objetoRetorno.Proxima);
        }

        [Fact]
        public async Task DeveRetornarListaGeneros_ComLinksParaAnteriorNullEProximoNull()
        {
            // Arrange
            var dicionarioGeneros = new Dictionary<string, string>()
            {
                { "Ação","acao" },
                { "Aventura","aventura" },
                { "Fantasia","fantasia" },
                { "Comédia","comedia" },
                { "Drama","drama" },
                { "Harém","harem" },
                { "Horror","horror" }
            };

            var generoAppServiceFactory = new GeneroAppServiceFactoryTestes();
            var listaGeneros = generoAppServiceFactory.GerarListaGeneros(dicionarioGeneros);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/generos";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            var retornoListaRetornoGeneroCadastrado = new List<RetornoGeneroCadastrado>();

            _generoServiceMock
                .Setup(x => x.RetornaListaGenerosCadastrados())
                .ReturnsAsync(listaGeneros);

            var listaGenerosCadastrados = await _generoAppService.RetornaListaGenerosCadastrados();
            var objetoRetorno = RequestHelper.CriarObjetoRetornoGenerosCadastrados(httpContext, listaGenerosCadastrados.ValueOrDefault, null, null);

            // Assert
            Assert.Equal(7, objetoRetorno.Data.Count);
            Assert.Null(objetoRetorno.Anterior);
            Assert.Null(objetoRetorno.Proxima);
        }

        [Fact]
        public async Task DeveRetornarZeroNoObjetoRetorno_QuandoGenerosNaoEncontrados_NaBuscaDeGeneros()
        {
            // Arrange
            var generoAppServiceFactory = new GeneroAppServiceFactoryTestes();
            var listaGeneros = new List<Genero>();

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/generos?skip=6&take=6";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            var retornoListaRetornoGeneroCadastrado = new List<RetornoGeneroCadastrado>();

            _generoServiceMock
                .Setup(x => x.RetornaListaGenerosCadastrados())
                .ReturnsAsync(listaGeneros);

            var listaGenerosCadastrados = await _generoAppService.RetornaListaGenerosCadastrados();
            var objetoRetorno = RequestHelper.CriarObjetoRetornoGenerosCadastrados(httpContext, listaGenerosCadastrados.ValueOrDefault, 6, 6);

            // Assert
            Assert.Empty(objetoRetorno.Data);
            Assert.Contains($"generos?Skip=0&Take=6", objetoRetorno.Anterior);
            Assert.Null(objetoRetorno.Proxima);
        }

        [Fact]
        public async Task DeveRetornarListaGeneros_ComTodosDados()
        {
            // Arrange
            var dicionarioGeneros = new Dictionary<string, string>()
            {
                { "Ação","acao" },
                { "Aventura","aventura" },
                { "Fantasia","fantasia" },
                { "Comédia","comedia" },
                { "Drama","drama" },
                { "Harém","harem" },
                { "Horror","horror" },
                { "Adulto","adulto" }
            };

            var generoAppServiceFactory = new GeneroAppServiceFactoryTestes();
            var listaGeneros = generoAppServiceFactory.GerarListaGenerosComTodosDados(dicionarioGeneros);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/generos?skip=1";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            _generoServiceMock
                .Setup(x => x.RetornaListaGeneros())
                .ReturnsAsync(listaGeneros);

            var listaGenerosCadastrados = await _generoAppService.RetornaListaGeneros();
            var objetoRetorno = RequestHelper.CriarObjetoRetornoGeneros(httpContext, listaGenerosCadastrados.ValueOrDefault, null, null);

            var numeroDeCamposRetornoGenero = objetoRetorno.Data.First().GetType().GetProperties().Length;

            // Assert
            Assert.Equal(8, objetoRetorno.Total);
            Assert.Contains($"generos?Skip=6&Take=6", objetoRetorno.Proxima);
            Assert.Equal(7, numeroDeCamposRetornoGenero);
            Assert.Equal("Ação", objetoRetorno.Data.First().Descricao);
            Assert.Equal("acao", objetoRetorno.Data.First().Slug);
            Assert.Equal("UsuarioTeste", objetoRetorno.Data.First().UsuarioAlteracao);
            Assert.Equal("UsuarioTeste", objetoRetorno.Data.First().UsuarioInclusao);
            Assert.NotEqual(new DateTime().ToString("dd/MM/yyyy HH:mm:ss"), objetoRetorno.Data.First().DataInclusao);
            Assert.NotEqual(new DateTime().ToString("dd/MM/yyyy HH:mm:ss"), objetoRetorno.Data.First().DataAlteracao);
        }
    }
}
