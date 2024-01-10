using Newtonsoft.Json;
using System.Net;
using TsundokuTraducoes.Api.DTOs.Admin;

namespace TsundokuTraducoes.Integration.Tests.Obras
{
    public class ComicTestesIntegracao : AppIntegrationBase
    {

        public MockComic _mockcomic;

        public ComicTestesIntegracao()
        {
            _mockcomic = new MockComic();
        }

        [Fact]
        public async Task DeveInserirUmaComic()
        {   
            var formData = _mockcomic.RetornaFormDataMockAdicionarComic(false);
            var response = await _httpClient.PostAsync("api/obra/comic", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var retornoSerializado = await response.Content.ReadAsStringAsync();
            var serializado = JsonConvert.DeserializeObject<ObraDTO>(retornoSerializado);

            _idObra = serializado.Id;
            _titulo = serializado.Titulo;
        }

        [Fact]
        public async Task DeveFalharAoInserirUmaComicSemTitulo()
        {            
            var formData = _mockcomic.RetornaFormDataMockAdicionarComic(true);

            var response = await _httpClient.PutAsync("api/obra/comic", formData);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeveAtualizarUmaComic()
        {
            await CarregaIdComicTitulo();

            var formData = _mockcomic.RetornaFormDataMockAtualizarComic(_idObra, _titulo, false);
            var response = await _httpClient.PutAsync("api/obra/comic", formData);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveFalharAoAtualizarUmaComicComCodigoHexaErrado()
        {  
            await CarregaIdComicTitulo();

            var formData = _mockcomic.RetornaFormDataMockAtualizarComic(_idObra, _titulo, true);
            var response = await _httpClient.PutAsync("api/obra/comic", formData);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarUmaComicPorId()
        {
            await CarregaIdComicTitulo();

            var response = await _httpClient.GetAsync($"api/obra/comic/{_idObra.ToString()}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarNotFoundParaComicNaoEncontrada()
        {
            var idNovelInexistente = "97722a6d-2210-434b-ae48-1a3c6da4c7a2";
            var response = await _httpClient.GetAsync($"api/obra/comic/{idNovelInexistente}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        //[Fact(Skip = "Teste desativado, causa: erro de concorrência")]
        [Fact]
        public async Task DeveExcluirUmaComic()
        {
            await CarregaIdComicTitulo();

            var response = await _httpClient.DeleteAsync($"api/obra/comic/{_idObra}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarNotFoundAoExcluirUmaComiclInexistente()
        {
            var response = await _httpClient.DeleteAsync($"api/obra/comic/{_idObra}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarUmaListaDeComics()
        {
            var response = await _httpClient.GetAsync($"api/obra/comics");
            Assert.True(HttpStatusCode.OK == response.StatusCode || HttpStatusCode.NoContent == response.StatusCode);
        }
    }
}
