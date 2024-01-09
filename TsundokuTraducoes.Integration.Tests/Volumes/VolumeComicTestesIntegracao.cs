using System.Net;

namespace TsundokuTraducoes.Integration.Tests.Volumes
{
    public class VolumeComicTestesIntegracao : AppIntegrationBase
    {
        public MockVolumeComic _mockVolumeComic;

        public VolumeComicTestesIntegracao()
        {
            _mockVolumeComic = new MockVolumeComic();
        }

        [Fact]
        public async Task DeveInserirUmVolumeComic()
        {
            await CarregaIdComic();

            var formData = _mockVolumeComic.RetornaFormDataMockAdicionarVolumeComic(false, _idObra);
            var response = await _httpClient.PostAsync("api/volume/comic", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task DeveFalharAoInserirUmVolumeComicSemNumero()
        {
            await CarregaIdComic();

            var formData = _mockVolumeComic.RetornaFormDataMockAdicionarVolumeComic(true, _idObra);
            var response = await _httpClient.PostAsync("api/volume/comic", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeveAtualizarUmVolumeComic()
        {
            await CarregaIdVolumeComic();

            var formData = _mockVolumeComic.RetornaFormDataMockAtualizarVolumeComic(false, _idObra, _idVolume);
            var response = await _httpClient.PutAsync("api/volume/comic", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveFalharAoAtualizarUmVolumeComicSemNumeroESemLoginAlteracao()
        {
            await CarregaIdVolumeComic();

            var formData = _mockVolumeComic.RetornaFormDataMockAtualizarVolumeComic(true, _idObra, _idVolume);
            var response = await _httpClient.PutAsync("api/volume/comic", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarUmVolumeComicPorId()
        {
            await CarregaIdVolumeComic();

            var response = await _httpClient.GetAsync($"api/volume/comic/{_idVolume}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarNotFoundParaVolumeComicNaoEncontrada()
        {
            var response = await _httpClient.GetAsync($"api/volume/comic/{_idVolume.GetValueOrDefault()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact(Skip = "Teste desativado, causa: erro de concorrência")]
        public async Task DeveExcluirUmVolumeComic()
        {
            await CarregaIdVolumeComic();

            var response = await _httpClient.DeleteAsync($"api/volume/comic/{_idVolume}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarNotFoundAoExcluirUmVolumeComicInexistente()
        {
            var response = await _httpClient.DeleteAsync($"api/obra/comic/{_idVolume.GetValueOrDefault()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarUmaListaDeVolumeComicsOuSemConteudo()
        {
            var response = await _httpClient.GetAsync($"api/volume/comic");
            Assert.True(HttpStatusCode.OK == response.StatusCode || HttpStatusCode.NoContent == response.StatusCode);
        }
    }
}
