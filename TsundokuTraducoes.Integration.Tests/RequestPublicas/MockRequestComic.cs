using TsundokuTraducoes.Integration.Tests.Recursos;

namespace TsundokuTraducoes.Integration.Tests.RequestPublicas
{
    public static class MockRequestComic
    {
        public static MultipartFormDataContent RetornaFormDataMockAdicionarComic(string generos)
        {
            var form = new MultipartFormDataContent();
            var titulo = $"Kage no Jitsuryokusha ni Naritakute - Teste - {Guid.NewGuid().ToString().Substring(0, 8)}";

            form.Add(new StringContent(titulo), "Titulo");
            form.Add(new StringContent("Shadow"), "Alias");
            form.Add(new StringContent("A Eminência nas Sombras, The Eminence in Shadow, To Be a Power in The Shadow"), "TituloAlternativo");
            form.Add(new StringContent("Aizawa Daisuke"), "Autor");
            form.Add(new StringContent("SAKANO Anri"), "Artista");
            form.Add(new StringContent("2018"), "Ano");
            form.Add(new StringContent("Bravo"), "UsuarioInclusao");
            form.Add(new StringContent("Da mesma forma que todos já adoraram heróis em sua infância, um certo jovem admirava aqueles que agiam nas sombras."), "Sinopse");
            form.Add(new StringContent("false"), "EhObraMaiorIdade");
            form.Add(new StringContent(generos), "ListaGeneros");
            form.Add(new StringContent("#81F7F3"), "CodigoCorHexaObra");
            form.Add(new StringContent("japonesa"), "NacionalidadeSlug");
            form.Add(new StringContent("em-andamento"), "StatusObraSlug");
            form.Add(new StringContent("manga"), "TipoObraSlug");
            form.Add(new StringContent("false"), "EhRecomendacao");

            var contentImagemPrincipal = MockBase.RetornaStreamImagemMock("imagemPrincipal.jpeg", "ImagemCapaPrincipalFile");
            var contentImagemBanner = MockBase.RetornaStreamImagemMock("imagemBanner.jpeg", "ImagemBannerFile");
            form.Add(contentImagemPrincipal);
            form.Add(contentImagemBanner);

            return form;
        }
    }
}