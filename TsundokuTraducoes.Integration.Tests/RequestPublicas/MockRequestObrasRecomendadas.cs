using TsundokuTraducoes.Integration.Tests.Recursos;

namespace TsundokuTraducoes.Integration.Tests.RequestPublicas
{
    public class MockRequestObrasRecomendadas
    {
        public static MultipartFormDataContent RetornaFormDataMockAdicionaComic(int quantidadeCapitulosComic)
        {
            var form = new MultipartFormDataContent();
            var titulo = $"Comic Teste Volume - {Guid.NewGuid().ToString().Substring(0, 8)}_{Guid.NewGuid().ToString().Substring(0, 8)}";

            var recomendada = "false";
            if (quantidadeCapitulosComic == 1)
            {
                recomendada = "true";
            }

            form.Add(new StringContent(titulo), "Titulo");
            form.Add(new StringContent(titulo), "Alias");
            form.Add(new StringContent("A Eminência nas Sombras, The Eminence in Shadow, To Be a Power in The Shadow"), "TituloAlternativo");
            form.Add(new StringContent("Aizawa Daisuke"), "Autor");
            form.Add(new StringContent("SAKANO Anri"), "Artista");
            form.Add(new StringContent("2018"), "Ano");
            form.Add(new StringContent("Bravo"), "UsuarioInclusao");
            form.Add(new StringContent("Da mesma forma que todos já adoraram heróis em sua infância, um certo jovem admirava aqueles que agiam nas sombras."), "Sinopse");
            form.Add(new StringContent("false"), "EhObraMaiorIdade");
            form.Add(new StringContent("fantasia,aventura,drama"), "ListaGeneros");
            form.Add(new StringContent("#81F7F3"), "CodigoCorHexaObra");
            form.Add(new StringContent("japonesa"), "NacionalidadeSlug");
            form.Add(new StringContent("em-andamento"), "StatusObraSlug");
            form.Add(new StringContent("manga"), "TipoObraSlug");
            form.Add(new StringContent(recomendada), "EhRecomendacao");

            var contentImagemPrincipal = MockBase.RetornaStreamImagemMock("imagemPrincipal.jpeg", "ImagemCapaPrincipalFile");
            var contentImagemBanner = MockBase.RetornaStreamImagemMock("imagemBanner.jpeg", "ImagemBannerFile");
            form.Add(contentImagemPrincipal);
            form.Add(contentImagemBanner);

            return form;
        }

        public static MultipartFormDataContent RetornaFormDataMockAdicionaNovel(int quantidadeCapitulosNovel)
        {
            var form = new MultipartFormDataContent();
            var titulo = $"Novel Teste Capitulo - {Guid.NewGuid().ToString().Substring(0, 8)}_{Guid.NewGuid().ToString().Substring(0, 8)}";

            var recomendada = "false";
            if (quantidadeCapitulosNovel == 1)
            {
                recomendada = "true";
            }

            form.Add(new StringContent(titulo), "Titulo");
            form.Add(new StringContent(titulo), "Alias");
            form.Add(new StringContent("A Eminência nas Sombras, The Eminence in Shadow, To Be a Power in The Shadow"), "TituloAlternativo");
            form.Add(new StringContent("Aizawa Daisuke"), "Autor");
            form.Add(new StringContent("SAKANO Anri"), "Artista");
            form.Add(new StringContent("2018"), "Ano");
            form.Add(new StringContent("Bravo"), "UsuarioInclusao");
            form.Add(new StringContent("Da mesma forma que todos já adoraram heróis em sua infância, um certo jovem admirava aqueles que agiam nas sombras."), "Sinopse");
            form.Add(new StringContent("false"), "EhObraMaiorIdade");
            form.Add(new StringContent("fantasia,aventura,drama"), "ListaGeneros");
            form.Add(new StringContent("#81F7F3"), "CodigoCorHexaObra");
            form.Add(new StringContent("japonesa"), "NacionalidadeSlug");
            form.Add(new StringContent("em-andamento"), "StatusObraSlug");
            form.Add(new StringContent("manga"), "TipoObraSlug");
            form.Add(new StringContent(recomendada), "EhRecomendacao");

            var contentImagemPrincipal = MockBase.RetornaStreamImagemMock("imagemPrincipal.jpeg", "ImagemCapaPrincipalFile");
            var contentImagemBanner = MockBase.RetornaStreamImagemMock("imagemBanner.jpeg", "ImagemBannerFile");
            form.Add(contentImagemPrincipal);
            form.Add(contentImagemBanner);

            return form;
        }
    }
}