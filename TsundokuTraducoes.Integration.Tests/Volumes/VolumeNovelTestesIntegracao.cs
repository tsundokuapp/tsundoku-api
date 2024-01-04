using System.Net;

namespace TsundokuTraducoes.Integration.Tests.Volumes
{
    #nullable disable

    public class VolumeNovelTestesIntegracao : AppIntegrationBase
    {
        public MockVolumeNovel _mockVolumeNovel;

        public VolumeNovelTestesIntegracao()
        {
            _mockVolumeNovel = new MockVolumeNovel();
        }

        [Fact]
        public async Task DeveInserirUmVolumeNovel()
        {
            await CarregaIdNovel();

            var formData = _mockVolumeNovel.RetornaFormDataMockAdicionarVolumeNovel(false, _idObra);
            var response = await _httpClient.PostAsync("api/volume/novel", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task DeveFalharAoInserirUmaNovelSemNumero()
        {
            await CarregaIdNovel();

            var formData = _mockVolumeNovel.RetornaFormDataMockAdicionarVolumeNovel(true, _idObra);
            var response = await _httpClient.PostAsync("api/volume/novel", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeveAtualizarUmVolumeNovel()
        {
            await CarregaIdVolumeNovel();

            var formData = _mockVolumeNovel.RetornaFormDataMockAtualizarVolumeNovel(false, _idObra, _idVolume);
            var response = await _httpClient.PutAsync("api/volume/novel", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveFalharAoAtualizarUmVolumeNovelSemNumeroESemLoginAlteracao()
        {
            await CarregaIdVolumeNovel();

            var formData = _mockVolumeNovel.RetornaFormDataMockAtualizarVolumeNovel(true, _idObra, _idVolume);
            var response = await _httpClient.PutAsync("api/volume/novel", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarUmVolumeNovelPorId()
        {
            await CarregaIdVolumeNovel();

            var response = await _httpClient.GetAsync($"api/volume/novel/{_idVolume}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarNotFoundParaVolumeNovelNaoEncontrada()
        {
            var response = await _httpClient.GetAsync($"api/volume/novel/{_idVolume.GetValueOrDefault()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeveExcluirUmVolumeNovel()
        {
            await CarregaIdVolumeNovel();
            
            var response = await _httpClient.DeleteAsync($"api/volume/novel/{_idVolume}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarNotFoundAoExcluirUmVolumeNovelInexistente()
        {
            var response = await _httpClient.DeleteAsync($"api/obra/novel/{_idVolume.GetValueOrDefault()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarUmaListaDeVolumeNovelsOuSemConteudo()
        {
            var response = await _httpClient.GetAsync($"api/volume/novel");
            Assert.True(HttpStatusCode.OK == response.StatusCode || HttpStatusCode.NoContent == response.StatusCode);
        }
    }
}
