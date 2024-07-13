using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Integration.Tests.Capitulos
{
    public class CapituloComicTestesIntegracao
    {
        private readonly HttpClient _httpClient;

        public CapituloComicTestesIntegracao()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        [Fact]
        public async Task DeveInserirUmCapituloComic()
        {
            var retornoObra = await AdicionaComic();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);

            var formData = MockCapituloComic.RetornaFormDataMockAdicionarCapituloComic(false, retornoVolume.Id);
            var response = await _httpClient.PostAsync("api/capitulo/comic", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task DeveFalharAoInserirUmCapituloComicSemImagens()
        {
            var retornoObra = await AdicionaComic();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);

            var formData = MockCapituloComic.RetornaFormDataMockAdicionarCapituloComic(true, retornoVolume.Id);
            var response = await _httpClient.PostAsync("api/capitulo/comic", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeveAtualizarUmCapituloComic()
        {
            var retornoObra = await AdicionaComic();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);
            var retornoCapitulo = await AdicionaCapitulo(retornoVolume.Id);

            var formData = MockCapituloComic.RetornaFormDataMockAtualizarCapituloComic(false, retornoVolume.Id, retornoCapitulo.Id);
            var response = await _httpClient.PutAsync("api/capitulo/comic", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveFalharAoAtualizarUmCapituloComicSemImagensAlteracao()
        {
            var retornoObra = await AdicionaComic();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);
            var retornoCapitulo = await AdicionaCapitulo(retornoVolume.Id);

            var formData = MockCapituloComic.RetornaFormDataMockAtualizarCapituloComic(true, retornoVolume.Id, retornoCapitulo.Id);
            var response = await _httpClient.PutAsync("api/capitulo/comic", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarUmCapituloComicPorId()
        {
            var retornoObra = await AdicionaComic();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);
            var retornoCapitulo = await AdicionaCapitulo(retornoVolume.Id);

            var response = await _httpClient.GetAsync($"api/capitulo/comic/{retornoCapitulo.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarNotFoundParaCapituloComicNaoEncontrada()
        {
            var response = await _httpClient.GetAsync($"api/capitulo/comic/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeveExcluirUmCapituloComic()
        {
            var retornoObra = await AdicionaComic();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);
            var retornoCapitulo = await AdicionaCapitulo(retornoVolume.Id);

            var response = await _httpClient.DeleteAsync($"api/capitulo/comic/{retornoCapitulo.Id}/true");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarNotFoundAoExcluirUmCapituloComicInexistente()
        {
            var response = await _httpClient.DeleteAsync($"api/capitulo/comic/{Guid.NewGuid()}/true");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarUmaListaDeCapituloComics()
        {
            var response = await _httpClient.GetAsync($"api/capitulo/comic?skip=&take=");
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }


        public async Task<RetornoObra> AdicionaComic()
        {
            var formData = MockCapituloComic.RetornaFormDataMockAdicionaComic();
            var response = await _httpClient.PostAsync("api/obra/comic", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir uma comic para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoObra>(content);
        }

        public async Task<RetornoVolume> AdicionaVolume(Guid obraId)
        {
            var formData = MockCapituloComic.RetornaFormDataMockAdicionarVolumeComic(obraId);
            var response = await _httpClient.PostAsync("api/volume/comic", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir um volume de comic para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoVolume>(content);
        }

        public async Task<RetornoCapitulo> AdicionaCapitulo(Guid volumeId)
        {
            var formData = MockCapituloComic.RetornaFormDataMockAdicionarCapituloComic(false, volumeId);
            var response = await _httpClient.PostAsync("api/capitulo/comic", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir um capitulo de comic para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoCapitulo>(content);
        }
    }
}
