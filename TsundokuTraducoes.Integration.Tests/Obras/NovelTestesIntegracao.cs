using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using TsundokuTraducoes.Helpers;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Integration.Tests.Obras
{
    public class NovelTestesIntegracao
    {
        private readonly HttpClient _httpClient;

        public NovelTestesIntegracao()
        {  
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        [Fact]
        public async Task DeveInserirUmaNovel()
        {            
            var formData = MockNovel.RetornaFormDataMockAdicionarNovel(false);
            var response = await _httpClient.PostAsync("api/obra/novel", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var retornoObra = JsonConvert.DeserializeObject<RetornoObra>(stringResponse);
            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        [Fact]
        public async Task DeveFalharAoInserirUmaNovelSemTitulo()
        {            
            var formData = MockNovel.RetornaFormDataMockAdicionarNovel(true);
            var response = await _httpClient.PostAsync("api/obra/novel", formData);
            
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeveAtualizarUmaNovel()
        {
            var retornoObra = await AdicionaObraParaAtualizar();
            var formData = MockNovel.RetornaFormDataMockAtualizarNovel(retornoObra.Id, retornoObra.Titulo, false);
            var response = await _httpClient.PutAsync("api/obra/novel", formData);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }        

        [Fact]
        public async Task DeveFalharAoAtualizarUmaNovelComCodigoHexaErrado()
        {
            var retornoObra = await AdicionaObraParaAtualizarEFalhar();
            var formData = MockNovel.RetornaFormDataMockAtualizarNovel(retornoObra.Id, retornoObra.Titulo, true);
            var response = await _httpClient.PutAsync("api/obra/novel", formData);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        [Fact]
        public async Task DeveRetornarUmaNovelPorId()
        {
            var retornoObra = await AdicionaObraParaRetornarUmaNovelPorId();
            var response = await _httpClient.GetAsync($"api/obra/novel/{retornoObra.Id}");           
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        [Fact]
        public async Task DeveRetornarNotFoundParaNovelNaoEncontrada()
        {
            var idNovelInexistente = "97722a6d-2210-434b-ae48-1a3c6da4c7a2";
            var response = await _httpClient.GetAsync($"api/obra/novel/{idNovelInexistente}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeveExcluirUmaNovel()
        {
            var retornoObra = await AdicionaObraParaExclurUmaNovel();
            var response = await _httpClient.DeleteAsync($"api/obra/novel/{retornoObra.Id}/true");
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        [Fact]
        public async Task DeveRetornarNotFoundAoExcluirUmaNovelInexistente()
        {
            var idNovelInexistente = "97722a6d-2210-434b-ae48-1a3c6da4c7a2";
            var response = await _httpClient.DeleteAsync($"api/obra/novel/{idNovelInexistente}/true");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarUmaListaDeNovels()
        {
            var response = await _httpClient.GetAsync($"api/obra/novels?skip=&take=");
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }


        private async Task<RetornoObra> AdicionaObraParaAtualizar()
        {
            var formData = MockNovel.RetornaFormDataMockAdicionarNovelAtualizar();
            var response = await _httpClient.PostAsync("api/obra/novel", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao tentar adicionar uma novel para teste e atualizar");

            var retornoAdicaoNovel = await response.Content.ReadAsStringAsync();
            var retornoObra = JsonConvert.DeserializeObject<RetornoObra>(retornoAdicaoNovel);

            return retornoObra;
        }

        private async Task<RetornoObra> AdicionaObraParaAtualizarEFalhar()
        {
            var formData = MockNovel.RetornaFormDataMockAdicionarNovelAtualizarFalhar();
            var response = await _httpClient.PostAsync("api/obra/novel", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao tentar adicionar uma novel para teste de atualizar e falhar");

            var retornoAdicaoNovel = await response.Content.ReadAsStringAsync();
            var retornoObra = JsonConvert.DeserializeObject<RetornoObra>(retornoAdicaoNovel);

            return retornoObra;
        }

        private async Task<RetornoObra> AdicionaObraParaRetornarUmaNovelPorId()
        {
            var formData = MockNovel.RetornaFormDataMockAdicionaObraParaRetornarUmaNovelPorId();
            var response = await _httpClient.PostAsync("api/obra/novel", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao tentar adicionar uma novel para teste de retornar Novel por Id");

            var retornoAdicaoNovel = await response.Content.ReadAsStringAsync();
            var retornoObra = JsonConvert.DeserializeObject<RetornoObra>(retornoAdicaoNovel);

            return retornoObra;
        }

        private async Task<RetornoObra> AdicionaObraParaExclurUmaNovel()
        {
            var formData = MockNovel.RetornaFormDataMockAdicionaObraParaExclurUmaNovel();
            var response = await _httpClient.PostAsync("api/obra/novel", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao tentar adicionar uma novel para teste de exclusão de novel");

            var retornoAdicaoNovel = await response.Content.ReadAsStringAsync();
            var retornoObra = JsonConvert.DeserializeObject<RetornoObra>(retornoAdicaoNovel);

            return retornoObra;
        }
    }
}