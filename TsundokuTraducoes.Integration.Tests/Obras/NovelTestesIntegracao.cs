using Newtonsoft.Json;
using System.Net;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Integration.Tests.Obras
{
    public class NovelTestesIntegracao : AppIntegrationBase
    {
        public MockNovel _mockNovel;

        public NovelTestesIntegracao()
        {
            _mockNovel = new MockNovel();
        }

        [Fact]
        public async Task DeveInserirUmaNovel()
        {            
            var formData = _mockNovel.RetornaFormDataMockAdicionarNovel(false);
            var response = await _httpClient.PostAsync("api/obra/novel", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var retornoSerializado = await response.Content.ReadAsStringAsync();
            var serializado = JsonConvert.DeserializeObject<ObraDTO>(retornoSerializado);

            _idObra = serializado.Id;
            _titulo = serializado.Titulo;
        }

        [Fact]
        public async Task DeveFalharAoInserirUmaNovelSemTitulo()
        {            
            var formData = _mockNovel.RetornaFormDataMockAdicionarNovel(true);
            var response = await _httpClient.PutAsync("api/obra/novel", formData);
            
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeveAtualizarUmaNovel()
        {          
            await CarregaIdNovelTitulo();

            var formData = _mockNovel.RetornaFormDataMockAtualizarNovel(_idObra, _titulo, false);
            var response = await _httpClient.PutAsync("api/obra/novel", formData);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }        

        [Fact]
        public async Task DeveFalharAoAtualizarUmaNovelComCodigoHexaErrado()
        {
            await CarregaIdNovelTitulo();

            var formData = _mockNovel.RetornaFormDataMockAtualizarNovel(_idObra, _titulo, true);
            var response = await _httpClient.PutAsync("api/obra/novel", formData);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarUmaNovelPorId()
        {
            await CarregaIdNovelTitulo();

            var response = await _httpClient.GetAsync($"api/obra/novel/{_idObra.ToString()}");           
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);            
        }

        [Fact]
        public async Task DeveRetornarNotFoundParaNovelNaoEncontrada()
        {
            var idNovelInexistente = "97722a6d-2210-434b-ae48-1a3c6da4c7a2";
            var response = await _httpClient.GetAsync($"api/obra/novel/{idNovelInexistente}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        //[Fact(Skip = "Teste desativado, causa: erro de concorrência")]
        [Fact]
        public async Task DeveExcluirUmaNovel()
        {
            await CarregaIdNovelTitulo();

            var response = await _httpClient.DeleteAsync($"api/obra/novel/{_idObra}");
          
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarNotFoundAoExcluirUmaNovelInexistente()
        {
            var response = await _httpClient.DeleteAsync($"api/obra/novel/{_idObra}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarUmaListaDeNovels()
        {
            var response = await _httpClient.GetAsync($"api/obra/novels");
            Assert.True(HttpStatusCode.OK == response.StatusCode || HttpStatusCode.NoContent == response.StatusCode);
        }        
    }
}
