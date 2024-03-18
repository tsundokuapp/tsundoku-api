using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Integration.Tests.Obras
{
    public class ComicTestesIntegracao
    {
        private readonly HttpClient _httpClient;

        public ComicTestesIntegracao()
        {   
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        [Fact]
        public async Task DeveInserirUmaComic()
        {   
            var formData = MockComic.RetornaFormDataMockAdicionarComic(false);
            var response = await _httpClient.PostAsync("api/obra/comic", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task DeveFalharAoInserirUmaComicSemTitulo()
        {            
            var formData = MockComic.RetornaFormDataMockAdicionarComic(true);
            var response = await _httpClient.PostAsync("api/obra/comic", formData);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeveAtualizarUmaComic()
        {
            var retornoObra = await AdicionaObraParaAtualizar();
            var formData = MockComic.RetornaFormDataMockAtualizarComic(retornoObra.Id, retornoObra.Titulo, retornoObra.Alias, false);
            var response = await _httpClient.PutAsync("api/obra/comic", formData);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveFalharAoAtualizarUmaComicComCodigoHexaErrado()
        {
            var retornoObra = await AdicionaObraParaAtualizarEFalhar();
            var formData = MockComic.RetornaFormDataMockAtualizarComic(retornoObra.Id, retornoObra.Titulo, retornoObra.Alias, true);
            var response = await _httpClient.PutAsync("api/obra/comic", formData);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }        

        [Fact]
        public async Task DeveRetornarUmaComicPorId()
        {
            var retornoObra = await AdicionaObraParaRetornarUmaComicPorId();
            var response = await _httpClient.GetAsync($"api/obra/comic/{retornoObra.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarNotFoundParaComicNaoEncontrada()
        {
            var idNovelInexistente = "97722a6d-2210-434b-ae48-1a3c6da4c7a2";
            var response = await _httpClient.GetAsync($"api/obra/comic/{idNovelInexistente}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        
        [Fact]
        public async Task DeveExcluirUmaComic()
        {
            var retornoObra = await AdicionaObraParaExclurUmaComic();
            var response = await _httpClient.DeleteAsync($"api/obra/comic/{retornoObra.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarNotFoundAoExcluirUmaComiclInexistente()
        {
            var idNovelInexistente = "97722a6d-2210-434b-ae48-1a3c6da4c7a2";
            var response = await _httpClient.DeleteAsync($"api/obra/comic/{idNovelInexistente}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarUmaListaDeComics()
        {
            var response = await _httpClient.GetAsync($"api/obra/comics?skip=&take=");
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        private async Task<RetornoObra> AdicionaObraParaAtualizar()
        {
            var formData = MockComic.RetornaFormDataMockAdicionarComicAtualizar();
            var response = await _httpClient.PostAsync("api/obra/comic", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao tentar adicionar uma comic para teste e atualizar");

            var retornoAdicaoComic = await response.Content.ReadAsStringAsync();
            var retornoObra = JsonConvert.DeserializeObject<RetornoObra>(retornoAdicaoComic);

            return retornoObra;
        }

        private async Task<RetornoObra> AdicionaObraParaAtualizarEFalhar()
        {
            var formData = MockComic.RetornaFormDataMockAdicionarComicAtualizarFalhar();
            var response = await _httpClient.PostAsync("api/obra/comic", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao tentar adicionar uma comic para teste de atualizar e falhar");

            var retornoAdicaoComic = await response.Content.ReadAsStringAsync();
            var retornoObra = JsonConvert.DeserializeObject<RetornoObra>(retornoAdicaoComic);

            return retornoObra;
        }

        private async Task<RetornoObra> AdicionaObraParaRetornarUmaComicPorId()
        {
            var formData = MockComic.RetornaFormDataMockAdicionaObraParaRetornarUmaComicPorId();
            var response = await _httpClient.PostAsync("api/obra/comic", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao tentar adicionar uma comic para teste de retornar Comic por Id");

            var retornoAdicaoComic = await response.Content.ReadAsStringAsync();
            var retornoObra = JsonConvert.DeserializeObject<RetornoObra>(retornoAdicaoComic);

            return retornoObra;
        }

        private async Task<RetornoObra> AdicionaObraParaExclurUmaComic()
        {
            var formData = MockComic.RetornaFormDataMockAdicionaObraParaExclurUmaComic();
            var response = await _httpClient.PostAsync("api/obra/comic", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao tentar adicionar uma comic para teste de exclusão de comic");

            var retornoAdicaoComic = await response.Content.ReadAsStringAsync();
            var retornoObra = JsonConvert.DeserializeObject<RetornoObra>(retornoAdicaoComic);

            return retornoObra;
        }
    }
}