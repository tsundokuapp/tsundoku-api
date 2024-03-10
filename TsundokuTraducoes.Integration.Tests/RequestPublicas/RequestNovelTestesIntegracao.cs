using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using TsundokuTraducoes.Api.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Integration.Tests.RequestPublicas
{
    public class RequestNovelTestesIntegracao
    {
        private readonly HttpClient _httpClient;

        public RequestNovelTestesIntegracao()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        [Fact]
        public async Task DeveRetornarUmaListaDeNovels()
        {
            await AdicionaObra();

            var parametros = "pesquisar=&nacionalidade=japonesa&status=em-andamento&tipo=light-novel&genero=aventura&skip=&take=6";
            var response = await _httpClient.GetAsync($"api/obras/novels?{parametros}");
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarUmaListaDeNovelsPesquisa()
        {
            await AdicionaObra();

            var parametros = "pesquisar=Bruxa&nacionalidade=&status=&tipo=&genero=&skip=&take=6";
            var response = await _httpClient.GetAsync($"api/obras/novels?{parametros}");
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task DeveFalharRetornarUmaListaDeNovels()
        {
            var parametros = "pesquisar=&nacionalidade=&status=&tipo=&genero=&skip=&take=";
            var response = await _httpClient.GetAsync($"api/obras/novels?{parametros}");
            Assert.True(HttpStatusCode.BadRequest == response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarNovelPorId()
        {
            var novel = await AdicionaObra();
                        
            var response = await _httpClient.GetAsync($"api/obras/novel?IdObra={novel.Id}");
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task DeveFalharRetornarNovelPorId()
        {
            var response = await _httpClient.GetAsync($"api/obras/novel?IdObra={Guid.NewGuid()}");
            Assert.True(HttpStatusCode.NotFound == response.StatusCode);
        }

        private async Task<RetornoObra> AdicionaObra()
        {
            var listaRetorno = new List<RetornoObra>();
            var listaGenerosAgrupados = new string[] { "fantasia,aventura,drama", "acao,isekai,aventura", "comedia,fantasia,harem" };

            foreach (var generos in listaGenerosAgrupados)
            {
                var formData = MockResquestNovel.RetornaFormDataMockAdicionarNovel(generos);
                var response = await _httpClient.PostAsync("api/obra/novel", formData);

                if (!response.IsSuccessStatusCode)
                {
                    Assert.Fail("Falha ao tentar adicionar uma novel para teste e atualizar");
                    break;
                }

                var retornoAdicaoNovel = await response.Content.ReadAsStringAsync();
                var retornoObra = JsonConvert.DeserializeObject<RetornoObra>(retornoAdicaoNovel);

                listaRetorno.Add(retornoObra);
            }

            return listaRetorno.FirstOrDefault();
        }
    }
}
