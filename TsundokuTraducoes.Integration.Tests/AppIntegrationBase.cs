using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

#nullable disable

namespace TsundokuTraducoes.Integration.Tests
{
    public class AppIntegrationBase
    {
        protected HttpClient _httpClient;
        protected Guid _idObra;
        protected string _titulo;
        protected Guid? _idVolume;

        public AppIntegrationBase()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        protected async Task CarregaIdNovelTitulo()
        {
            var responseNovels = await _httpClient.GetAsync($"api/obra/novels");

            var retornoSerializado = await responseNovels.Content.ReadAsStringAsync();
            var serializado = JsonConvert.DeserializeObject<List<RetornoObra>>(retornoSerializado)?.FirstOrDefault();

            _idObra = serializado.Id;
            _titulo = serializado.Titulo;
        }

        protected async Task CarregaIdComicTitulo()
        {
            var responseNovels = await _httpClient.GetAsync($"api/obra/comics");

            var retornoSerializado = await responseNovels.Content.ReadAsStringAsync();
            var serializado = JsonConvert.DeserializeObject<List<RetornoObra>>(retornoSerializado)?.FirstOrDefault();

            _idObra = serializado.Id;
            _titulo = serializado.Titulo;
        }

        protected HttpContent RetornaStreamImagemMock(string nomeArquivo, string idForm)
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
    
        protected async Task CarregaIdNovel()
        {
            var responseNovels = await _httpClient.GetAsync($"api/obra/novels");

            var retornoSerializado = await responseNovels.Content.ReadAsStringAsync();
            var listaNovels = JsonConvert.DeserializeObject<List<RetornoObra>>(retornoSerializado);
            var serializado = listaNovels?.FirstOrDefault();

            _idObra = serializado.Id;
        }

        protected async Task CarregaIdVolumeNovel()
        {
            var responseNovels = await _httpClient.GetAsync($"api/volume/novel");

            var retornoSerializado = await responseNovels.Content.ReadAsStringAsync();
            var listaVolumeNovels = JsonConvert.DeserializeObject<List<RetornoVolume>>(retornoSerializado);
            var serializado = listaVolumeNovels?.FirstOrDefault();

            _idVolume = serializado.Id;
            _idObra = serializado.NovelId.Value;
        }

        protected int RetornaNumeroAleatorio()
        {           
            return new Random().Next(1, 99);
        }
    }
}
