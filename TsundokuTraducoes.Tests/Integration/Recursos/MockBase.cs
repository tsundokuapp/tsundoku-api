using System.IO;
using System.Net.Http.Headers;

namespace TsundokuTraducoes.Integration.Tests.Recursos
{
    public static class MockBase
    {
        public static MemoryStream RetornaImagemTeste()
        {
            var diretorio = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "Recursos"), "assets", "images");

            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            var imagem = Path.Combine(diretorio, "ImagemTeste.png");
            var imagemByte = File.ReadAllBytes($"{imagem}");
            var stream = new MemoryStream(imagemByte);
            return stream;
        }

        public static MemoryStream RetornaImagemTesteFalha()
        {
            var diretorio = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "Recursos"), "assets", "images");

            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            var imagem = Path.Combine(diretorio, "ImagemTesteFalha.jpg");
            var imagemByte = File.ReadAllBytes($"{imagem}");
            var stream = new MemoryStream(imagemByte);

            return stream;
        }

        public static HttpContent RetornaStreamImagemMock(string nomeArquivo, string idForm)
        {
            var stream = RetornaImagemTeste();

            var httpcontent = new StreamContent(stream);
            httpcontent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = idForm,
                FileName = nomeArquivo
            };

            var contentType = new MediaTypeHeaderValue("image/png");
            httpcontent.Headers.ContentType = contentType;

            return httpcontent;
        }
    }
}