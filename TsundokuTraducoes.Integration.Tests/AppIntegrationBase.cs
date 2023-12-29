using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TsundokuTraducoes.Api.DTOs.Admin;

namespace TsundokuTraducoes.Integration.Tests
{
    public class AppIntegrationBase
    {
        protected HttpClient _httpClient;
        protected Guid _idObra;
        protected string _titulo;

        public AppIntegrationBase()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        protected async Task CarregaIdNovelTitulo()
        {
            var responseNovels = await _httpClient.GetAsync($"api/obra/novels");

            var retornoSerializado = await responseNovels.Content.ReadAsStringAsync();
            var serializado = JsonConvert.DeserializeObject<List<ObraDTO>>(retornoSerializado)?.FirstOrDefault();

            _idObra = serializado.Id;
            _titulo = serializado.Titulo;
        }

        protected async Task CarregaIdComicTitulo()
        {
            var responseNovels = await _httpClient.GetAsync($"api/obra/comics");

            var retornoSerializado = await responseNovels.Content.ReadAsStringAsync();
            var serializado = JsonConvert.DeserializeObject<List<ObraDTO>>(retornoSerializado)?.FirstOrDefault();

            _idObra = serializado.Id;
            _titulo = serializado.Titulo;
        }

        public HttpContent RetornaStreamImagemMock(string nomeArquivo, string idForm)
        {
            var stream = new MemoryStream();

            var httpcontent = new StreamContent(stream);
            httpcontent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = idForm,
                FileName = nomeArquivo,

            };

            var teste = new MediaTypeHeaderValue("image/jpg");
            httpcontent.Headers.ContentType = teste;

            return httpcontent;
        }
    }
}
