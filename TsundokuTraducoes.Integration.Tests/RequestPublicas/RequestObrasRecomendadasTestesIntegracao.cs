using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Integration.Tests.RequestPublicas
{
    public class RequestObrasRecomendadasTestesIntegracao
    {
        private readonly HttpClient _httpClient;

        public RequestObrasRecomendadasTestesIntegracao()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        [Fact]
        public async Task DeveRetornarUmaListaDeObrasRecomendadas()
        {
            var quantidadeCapitulosComic = 0;
            while (quantidadeCapitulosComic < 3)
            {
                await AdicionaComic(quantidadeCapitulosComic);
                quantidadeCapitulosComic++;
                Thread.Sleep(500);
            }

            var quantidadeCapitulosNovel = 0;
            while (quantidadeCapitulosNovel < 3)
            {
                await AdicionaNovel(quantidadeCapitulosNovel);
                quantidadeCapitulosNovel++;
                Thread.Sleep(500);
            }

            var response = await _httpClient.GetAsync($"api/obras/recomendadas?skip=&take=4");
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        public async Task<RetornoObra> AdicionaComic(int quantidadeCapitulosComic)
        {
            var formData = MockRequestObrasRecomendadas.RetornaFormDataMockAdicionaComic(quantidadeCapitulosComic);
            var response = await _httpClient.PostAsync("api/admin/obra/comic", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir uma comic para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoObra>(content);
        }

        public async Task<RetornoObra> AdicionaNovel(int quantidadeCapitulosNovel)
        {
            var formData = MockRequestObrasRecomendadas.RetornaFormDataMockAdicionaNovel(quantidadeCapitulosNovel);
            var response = await _httpClient.PostAsync("api/admin/obra/novel", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir uma novel para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoObra>(content);
        }
    }
}