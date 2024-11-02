using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Integration.Tests.RequestPublicas
{
    public class RequestCapitulosPorIdTestesIntegracao
    {
        private readonly HttpClient _httpClient;

        public RequestCapitulosPorIdTestesIntegracao()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        [Fact]
        public async Task DeveRetornaCapituloComicPorId()
        {
            var retornoComic = await AdicionaComic();
            var retornoVolumeComic = await AdicionaVolumeComic(retornoComic.Id);

            var capitulo = await AdicionaCapituloComic(retornoVolumeComic.Id);

            var response = await _httpClient.GetAsync($"api/obras/comic/capitulo/{capitulo.Id}");
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornaCapituloNovelPorId()
        {            
            var retornoNovel = await AdicionaNovel();
            var retornoVolumeNovel = await AdicionaVolumeNovel(retornoNovel.Id);

            var capitulo = await AdicionaCapituloNovel(retornoVolumeNovel.Id);

            var response = await _httpClient.GetAsync($"api/obras/novel/capitulo/{capitulo.Id}");
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task DeveDarErroBadRequestRetornaCapituloComicPorId()
        {
            var response = await _httpClient.GetAsync($"api/obras/comic/capitulo/{Guid.NewGuid()}");
            Assert.True(HttpStatusCode.BadRequest == response.StatusCode);
        }

        [Fact]
        public async Task DeveDarErroBadRequestRetornaCapituloNovelPorId()
        {
            var response = await _httpClient.GetAsync($"api/obras/novel/capitulo/{Guid.NewGuid()}");
            Assert.True(HttpStatusCode.BadRequest == response.StatusCode);
        }

        public async Task<RetornoObra> AdicionaComic()
        {
            var formData = MockRequestHome.RetornaFormDataMockAdicionaComic();
            var response = await _httpClient.PostAsync("api/admin/obra/comic", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir uma comic para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoObra>(content);
        }

        public async Task<RetornoVolume> AdicionaVolumeComic(Guid obraId)
        {
            var formData = MockRequestHome.RetornaFormDataMockAdicionarVolumeComic(obraId);
            var response = await _httpClient.PostAsync("api/admin/volume/comic", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir um volume de comic para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoVolume>(content);
        }

        public async Task<RetornoCapitulo> AdicionaCapituloComic(Guid volumeId)
        {
            var formData = MockRequestHome.RetornaFormDataMockAdicionarCapituloComic(volumeId);
            var response = await _httpClient.PostAsync("api/admin/capitulo/comic", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir um capitulo de comic para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoCapitulo>(content);
        }

        public async Task<RetornoObra> AdicionaNovel()
        {
            var formData = MockRequestHome.RetornaFormDataMockAdicionaNovel();
            var response = await _httpClient.PostAsync("api/admin/obra/novel", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir uma novel para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoObra>(content);
        }

        public async Task<RetornoVolume> AdicionaVolumeNovel(Guid obraId)
        {
            var formData = MockRequestHome.RetornaFormDataMockAdicionarVolumeNovel(obraId);
            var response = await _httpClient.PostAsync("api/admin/volume/novel", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir um volume de novel para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoVolume>(content);
        }

        public async Task<RetornoCapitulo> AdicionaCapituloNovel(Guid volumeId)
        {
            var formData = MockRequestHome.RetornaFormDataMockAdicionarCapituloNovel(volumeId);
            var response = await _httpClient.PostAsync("api/admin/capitulo/novel", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir um capitulo de novel para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoCapitulo>(content);
        }
    }
}
