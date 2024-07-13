using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Integration.Tests.RequestPublicas
{
    public class RequestHomeTestesIntegracao
    {
        private readonly HttpClient _httpClient;

        public RequestHomeTestesIntegracao()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        [Fact]
        public async Task DeveRetornarUmaListaDeCapitulosParaHome()
        {
            var retornoComic = await AdicionaComic();
            var retornoVolumeComic = await AdicionaVolumeComic(retornoComic.Id);

            var quantidadeCapitulosComic = 0;
            while (quantidadeCapitulosComic < 3)
            {
                await AdicionaCapituloComic(retornoVolumeComic.Id);
                quantidadeCapitulosComic++;
                Thread.Sleep(250);
            }

            var retornoNovel = await AdicionaNovel();
            var retornoVolumeNovel = await AdicionaVolumeNovel(retornoNovel.Id);

            var quantidadeCapitulosNovel = 0;
            while (quantidadeCapitulosNovel < 3)
            {
                await AdicionaCapituloNovel(retornoVolumeNovel.Id);
                quantidadeCapitulosNovel++;
                Thread.Sleep(250);
            }

            var response = await _httpClient.GetAsync($"api/obras/home?skip=&take=6");
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        public async Task<RetornoObra> AdicionaComic()
        {
            var formData = MockRequestHome.RetornaFormDataMockAdicionaComic();
            var response = await _httpClient.PostAsync("api/obra/comic", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir uma comic para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoObra>(content);
        }

        public async Task<RetornoVolume> AdicionaVolumeComic(Guid obraId)
        {
            var formData = MockRequestHome.RetornaFormDataMockAdicionarVolumeComic(obraId);
            var response = await _httpClient.PostAsync("api/volume/comic", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir um volume de comic para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoVolume>(content);
        }

        public async Task<RetornoCapitulo> AdicionaCapituloComic(Guid volumeId)
        {
            var formData = MockRequestHome.RetornaFormDataMockAdicionarCapituloComic(volumeId);
            var response = await _httpClient.PostAsync("api/capitulo/comic", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir um capitulo de comic para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoCapitulo>(content);
        }

        public async Task<RetornoObra> AdicionaNovel()
        {
            var formData = MockRequestHome.RetornaFormDataMockAdicionaNovel();
            var response = await _httpClient.PostAsync("api/obra/novel", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir uma novel para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoObra>(content);
        }

        public async Task<RetornoVolume> AdicionaVolumeNovel(Guid obraId)
        {
            var formData = MockRequestHome.RetornaFormDataMockAdicionarVolumeNovel(obraId);
            var response = await _httpClient.PostAsync("api/volume/novel", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir um volume de novel para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoVolume>(content);
        }

        public async Task<RetornoCapitulo> AdicionaCapituloNovel(Guid volumeId)
        {
            var formData = MockRequestHome.RetornaFormDataMockAdicionarCapituloNovel(volumeId);
            var response = await _httpClient.PostAsync("api/capitulo/novel", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir um capitulo de novel para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoCapitulo>(content);
        }
    }
}