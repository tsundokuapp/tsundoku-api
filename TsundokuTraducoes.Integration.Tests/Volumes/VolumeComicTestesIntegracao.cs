using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Integration.Tests.Volumes
{
    public class VolumeComicTestesIntegracao
    {
        private readonly HttpClient _httpClient;

        public VolumeComicTestesIntegracao()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        [Fact]
        public async Task DeveInserirUmVolumeComic()
        {
            var retornoObra = await AdicionaComic();

            var formData = MockVolumeComic.RetornaFormDataMockAdicionarVolumeComic(false, retornoObra.Id);
            var response = await _httpClient.PostAsync("api/volume/comic", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task DeveFalharAoInserirUmVolumeComicSemNumero()
        {
            var retornoObra = await AdicionaComic();

            var formData = MockVolumeComic.RetornaFormDataMockAdicionarVolumeComic(true, retornoObra.Id);
            var response = await _httpClient.PostAsync("api/volume/comic", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeveAtualizarUmVolumeComic()
        {
            var retornoObra = await AdicionaComic();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);

            var formData = MockVolumeComic.RetornaFormDataMockAtualizarVolumeComic(false, retornoObra.Id, retornoVolume.Id);
            var response = await _httpClient.PutAsync("api/volume/comic", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveFalharAoAtualizarUmVolumeComicSemNumeroESemLoginAlteracao()
        {
            var retornoObra = await AdicionaComic();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);

            var formData = MockVolumeComic.RetornaFormDataMockAtualizarVolumeComic(true, retornoObra.Id, retornoVolume.Id);
            var response = await _httpClient.PutAsync("api/volume/comic", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarUmVolumeComicPorId()
        {
            var retornoObra = await AdicionaComic();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);

            var response = await _httpClient.GetAsync($"api/volume/comic/{retornoVolume.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarNotFoundParaVolumeComicNaoEncontrada()
        {
            var response = await _httpClient.GetAsync($"api/volume/comic/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
                
        [Fact]
        public async Task DeveExcluirUmVolumeComic()
        {
            var retornoObra = await AdicionaComic();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);

            var response = await _httpClient.DeleteAsync($"api/volume/comic/{retornoVolume.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarNotFoundAoExcluirUmVolumeComicInexistente()
        {
            var response = await _httpClient.DeleteAsync($"api/obra/comic/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarUmaListaDeVolumeComics()
        {
            var response = await _httpClient.GetAsync($"api/volume/comic");
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }
    
        
        public async Task<RetornoObra> AdicionaComic()
        {
            var formData = MockVolumeComic.RetornaFormDataMockAdicionaComic();
            var response = await _httpClient.PostAsync("api/obra/comic", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir uma comic para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoObra>(content);
        }

        public async Task<RetornoVolume> AdicionaVolume(Guid obraId)
        {
            var formData = MockVolumeComic.RetornaFormDataMockAdicionarVolumeComic(false, obraId);
            var response = await _httpClient.PostAsync("api/volume/comic", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir um volume de comic para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoVolume>(content);
        }
    }
}
