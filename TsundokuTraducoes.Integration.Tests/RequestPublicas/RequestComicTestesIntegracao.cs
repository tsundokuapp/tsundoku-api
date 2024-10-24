using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Integration.Tests.RequestPublicas
{
    public class RequestComicTestesIntegracao
    {
        private readonly HttpClient _httpClient;

        public RequestComicTestesIntegracao()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        [Fact]
        public async Task DeveRetornarUmaListaDeComics()
        {
            await AdicionaObra();

            var parametros = "pesquisar=&nacionalidade=japonesa&status=em-andamento&tipo=manga&genero=aventura&skip=&take=6";
            var response = await _httpClient.GetAsync($"api/obras/comics?{parametros}");
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarUmaListaDeComicsPesquisa()
        {
            await AdicionaObra();

            var parametros = "pesquisar=Kage no&nacionalidade=&status=&tipo=&genero=&skip=&take=6";
            var response = await _httpClient.GetAsync($"api/obras/comics?{parametros}");
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task DeveFalharRetornarUmaListaDeComics()
        {
            var parametros = "pesquisar=&nacionalidade=&status=&tipo=&genero=&skip=&take=";
            var response = await _httpClient.GetAsync($"api/obras/comics?{parametros}");
            Assert.True(HttpStatusCode.BadRequest == response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarComicPorId()
        {
            var comic = await AdicionaObra();

            var response = await _httpClient.GetAsync($"api/obras/comic?IdObra={comic.Id}");
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task DeveFalharRetornarComicPorId()
        {
            var response = await _httpClient.GetAsync($"api/obras/comic?IdObra={Guid.NewGuid()}");
            Assert.True(HttpStatusCode.NotFound == response.StatusCode);
        }

        private async Task<RetornoObra> AdicionaObra()
        {
            var listaRetorno = new List<RetornoObra>();
            var listaGenerosAgrupados = new string[] { "fantasia,aventura,drama", "acao,isekai,aventura", "comedia,fantasia,harem" };

            foreach (var generos in listaGenerosAgrupados)
            {
                var formData = MockRequestComic.RetornaFormDataMockAdicionarComic(generos);
                var response = await _httpClient.PostAsync("api/admin/obra/comic", formData);

                if (!response.IsSuccessStatusCode)
                {
                    Assert.Fail("Falha ao tentar adicionar uma comic para teste e atualizar");
                    break;
                }

                var retornoAdicaoComic = await response.Content.ReadAsStringAsync();
                var retornoObra = JsonConvert.DeserializeObject<RetornoObra>(retornoAdicaoComic);

                listaRetorno.Add(retornoObra);
            }

            return listaRetorno.FirstOrDefault();
        }
    }
}
