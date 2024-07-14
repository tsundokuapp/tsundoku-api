using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using TsundokuTraducoes.Helpers;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Integration.Tests.Volumes
{
    public class VolumeNovelTestesIntegracao
    {
        private readonly HttpClient _httpClient;

        public VolumeNovelTestesIntegracao()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        [Fact]
        public async Task DeveInserirUmVolumeNovel()
        {
            var retornoObra = await AdicionaNovel();

            var formData = MockVolumeNovel.RetornaFormDataMockAdicionarVolumeNovel(false, retornoObra.Id);
            var response = await _httpClient.PostAsync("api/volume/novel", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        [Fact]
        public async Task DeveFalharAoInserirUmaNovelSemNumero()
        {
            var retornoObra = await AdicionaNovel();

            var formData = MockVolumeNovel.RetornaFormDataMockAdicionarVolumeNovel(true, retornoObra.Id);
            var response = await _httpClient.PostAsync("api/volume/novel", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        [Fact]
        public async Task DeveAtualizarUmVolumeNovel()
        {
            var retornoObra = await AdicionaNovel();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);

            var formData = MockVolumeNovel.RetornaFormDataMockAtualizarVolumeNovel(false, retornoObra.Id, retornoVolume.Id);
            var response = await _httpClient.PutAsync("api/volume/novel", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        [Fact]
        public async Task DeveFalharAoAtualizarUmVolumeNovelSemNumeroESemLoginAlteracao()
        {
            var retornoObra = await AdicionaNovel();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);

            var formData = MockVolumeNovel.RetornaFormDataMockAtualizarVolumeNovel(true, retornoObra.Id, retornoVolume.Id);
            var response = await _httpClient.PutAsync("api/volume/novel", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        [Fact]
        public async Task DeveRetornarUmVolumeNovelPorId()
        {
            var retornoObra = await AdicionaNovel();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);

            var response = await _httpClient.GetAsync($"api/volume/novel/{retornoVolume.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        [Fact]
        public async Task DeveRetornarNotFoundParaVolumeNovelNaoEncontrada()
        {
            var response = await _httpClient.GetAsync($"api/volume/novel/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
                
        [Fact]
        public async Task DeveExcluirUmVolumeNovel()
        {
            var retornoObra = await AdicionaNovel();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);

            var response = await _httpClient.DeleteAsync($"api/volume/novel/{retornoVolume.Id}/true");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        [Fact]
        public async Task DeveRetornarNotFoundAoExcluirUmVolumeNovelInexistente()
        {
            var response = await _httpClient.DeleteAsync($"api/obra/novel/{Guid.NewGuid()}/true");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarUmaListaDeVolumeNovelsOuSemConteudo()
        {
            var response = await _httpClient.GetAsync($"api/volume/novel?skip=&take=");
            Assert.True(HttpStatusCode.OK == response.StatusCode || HttpStatusCode.NoContent == response.StatusCode);
        }

        public async Task<RetornoObra> AdicionaNovel()
        {
            var formData = MockVolumeNovel.RetornaFormDataMockAdicionaUmaNovel();
            var response = await _httpClient.PostAsync("api/obra/novel", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir uma novel para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoObra>(content);
        }

        public async Task<RetornoVolume> AdicionaVolume(Guid obraId)
        {
            var formData = MockVolumeNovel.RetornaFormDataMockAdicionarVolumeNovel(false, obraId);
            var response = await _httpClient.PostAsync("api/volume/novel", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir um volume de novel para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoVolume>(content);
        }
    }
}
