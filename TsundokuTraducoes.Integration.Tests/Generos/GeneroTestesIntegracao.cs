using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Integration.Tests.Generos
{
    public class GeneroTestesIntegracao
    {
        private readonly HttpClient _httpClient;

        public GeneroTestesIntegracao()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        [Fact]
        public async Task DeveInserirUmGenero()
        {
            var formData = MockGenero.RetornaFormDataMockAdicionarGenero();
            var response = await _httpClient.PostAsync("api/genero", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task DeveFalharAoInserirUmGenero()
        {
            var retornoGenero = await AdicionaGeneroParaTeste();
            var formData = MockGenero.RetornaFormDataMockAdicionarGenero(true, retornoGenero.Descricao);
            var response = await _httpClient.PostAsync("api/genero", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeveAtualizarUmGenero()
        {
            var retornoGenero = await AdicionaGeneroParaTeste();
            var formData = MockGenero.RetornaFormDataMockAtualizarGenero(false, retornoGenero.Id);
            var response = await _httpClient.PutAsync("api/genero", formData);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveFalharAtualizarUmGenero()
        {
            var retornoGenero = await AdicionaGeneroParaTeste();
            var formData = MockGenero.RetornaFormDataMockAtualizarGenero(true, retornoGenero.Id);
            var response = await _httpClient.PutAsync("api/genero", formData);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeveExcluirUmGenero()
        {
            var retornoGenero = await AdicionaGeneroParaTeste();
            var response = await _httpClient.DeleteAsync($"api/genero/{retornoGenero.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarNotFoundAoExcluirUmGenerolInexistente()
        {
            var idNovelInexistente = "97722a6d-2210-434b-ae48-1a3c6da4c7a2";
            var response = await _httpClient.DeleteAsync($"api/genero/{idNovelInexistente}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        private async Task<RetornoGenero> AdicionaGeneroParaTeste()
        {
            var formData = MockGenero.RetornaFormDataMockAdicionarGeneroParaTeste();
            var response = await _httpClient.PostAsync("api/genero", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao tentar adicionar uma genero para teste");

            var retornoAdicaoGenero = await response.Content.ReadAsStringAsync();
            var retornoGenero = JsonConvert.DeserializeObject<RetornoGenero>(retornoAdicaoGenero);

            return retornoGenero;
        }
    }
}
