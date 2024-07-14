using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using TsundokuTraducoes.Helpers;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Integration.Tests.Capitulos
{
    public class CapituloNovelTestesIntegracao
    {
        private readonly HttpClient _httpClient;

        public CapituloNovelTestesIntegracao()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        [Fact]
        public async Task DeveInserirUmCapituloNovel()
        {
            var retornoObra = await AdicionaNovel();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);

            var formData = MockCapituloNovel.RetornaFormDataMockAdicionarCapituloNovel(false, retornoVolume.Id);
            var response = await _httpClient.PostAsync("api/capitulo/novel", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        [Fact]
        public async Task DeveInserirUmCapituloNovelIlustracao()
        {
            var retornoObra = await AdicionaNovel();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);

            var formData = MockCapituloNovel.RetornaFormDataMockAdicionarCapituloNovelIlustracoes(false, retornoVolume.Id);
            var response = await _httpClient.PostAsync("api/capitulo/novel", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        [Fact]
        public async Task DeveFalharAoInserirUmCapituloNovel()
        {
            var retornoObra = await AdicionaNovel();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);

            var formData = MockCapituloNovel.RetornaFormDataMockAdicionarCapituloNovel(true, retornoVolume.Id);
            var response = await _httpClient.PostAsync("api/capitulo/novel", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        [Fact]
        public async Task DeveFalharAoInserirUmCapituloNovelIlustracaoSemImagens()
        {
            var retornoObra = await AdicionaNovel();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);

            var formData = MockCapituloNovel.RetornaFormDataMockAdicionarCapituloNovelIlustracoes(true, retornoVolume.Id);
            var response = await _httpClient.PostAsync("api/capitulo/novel", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        [Fact]
        public async Task DeveAtualizarUmCapituloNovel()
        {
            var retornoObra = await AdicionaNovel();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);
            var retornoCapitulo = await AdicionaCapitulo(retornoVolume.Id);

            var formData = MockCapituloNovel.RetornaFormDataMockAtualizarCapituloNovel(false, retornoVolume.Id, retornoCapitulo.Id);
            var response = await _httpClient.PutAsync("api/capitulo/novel", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        [Fact]
        public async Task DeveFalharAoAtualizarUmCapituloNovelSemImagensAlteracao()
        {
            var retornoObra = await AdicionaNovel();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);
            var retornoCapitulo = await AdicionaCapitulo(retornoVolume.Id);

            var formData = MockCapituloNovel.RetornaFormDataMockAtualizarCapituloNovel(true, retornoVolume.Id, retornoCapitulo.Id);
            var response = await _httpClient.PutAsync("api/capitulo/novel", formData);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        [Fact]
        public async Task DeveRetornarUmCapituloNovelPorId()
        {
            var retornoObra = await AdicionaNovel();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);
            var retornoCapitulo = await AdicionaCapitulo(retornoVolume.Id);

            var response = await _httpClient.GetAsync($"api/capitulo/novel/{retornoCapitulo.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        [Fact]
        public async Task DeveRetornarNotFoundParaCapituloNovelNaoEncontrada()
        {
            var response = await _httpClient.GetAsync($"api/capitulo/novel/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeveExcluirUmCapituloNovel()
        {
            var retornoObra = await AdicionaNovel();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);
            var retornoCapitulo = await AdicionaCapitulo(retornoVolume.Id);

            var response = await _httpClient.DeleteAsync($"api/capitulo/novel/{retornoCapitulo.Id}/true");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        [Fact]
        public async Task DeveRetornarNotFoundAoExcluirUmCapituloNovelInexistente()
        {
            var response = await _httpClient.DeleteAsync($"api/capitulo/novel/{Guid.NewGuid()}/true");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeveRetornarUmaListaDeCapituloNovels()
        {
            var retornoObra = await AdicionaNovel();
            var retornoVolume = await AdicionaVolume(retornoObra.Id);
            var retornoCapitulo = await AdicionaCapitulo(retornoVolume.Id);

            var response = await _httpClient.GetAsync($"api/capitulo/novel?skip=&take=");
            Assert.True(HttpStatusCode.OK == response.StatusCode);

            Diretorios.ExcluirDiretorioLocal(retornoObra.DiretorioImagemObra);
        }

        public async Task<RetornoObra> AdicionaNovel()
        {
            var formData = MockCapituloNovel.RetornaFormDataMockAdicionaNovel();
            var response = await _httpClient.PostAsync("api/obra/novel", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir uma novel para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoObra>(content);
        }

        public async Task<RetornoVolume> AdicionaVolume(Guid obraId)
        {
            var formData = MockCapituloNovel.RetornaFormDataMockAdicionarVolumeNovel(obraId);
            var response = await _httpClient.PostAsync("api/volume/novel", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir um volume de novel para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoVolume>(content);
        }

        public async Task<RetornoCapitulo> AdicionaCapitulo(Guid volumeId)
        {
            var formData = MockCapituloNovel.RetornaFormDataMockAdicionarCapituloNovel(false, volumeId);
            var response = await _httpClient.PostAsync("api/capitulo/novel", formData);

            if (!response.IsSuccessStatusCode)
                Assert.Fail("Falha ao inserir um capitulo de novel para teste");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RetornoCapitulo>(content);
        }
    }
}
