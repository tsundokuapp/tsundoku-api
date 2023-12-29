namespace TsundokuTraducoes.Integration.Tests.Obras
{
    public class MockComic : AppIntegrationBase
    {
        public MultipartFormDataContent RetornaFormDataMockAdicionarComic(bool falhar)
        {
            var form = new MultipartFormDataContent();
            var titulo = $"Kage no Jitsuryokusha ni Naritakute - Teste - {Guid.NewGuid().ToString().Substring(0, 8)}";

            if (falhar)
            {
                titulo = string.Empty;
            }

            form.Add(new StringContent(titulo), "Titulo");
            form.Add(new StringContent("Shadow"), "Alias");
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
            form.Add(new StringContent("false"), "EhRecomendacao");

            var contentImagemPrincipal = RetornaStreamImagemMock("imagemPrincipal.jpeg", "ImagemCapaPrincipalFile");
            var contentImagemBanner = RetornaStreamImagemMock("imagemBanner.jpeg", "ImagemBannerFile");
            form.Add(contentImagemPrincipal);
            form.Add(contentImagemBanner);

            return form;
        }

        public MultipartFormDataContent RetornaFormDataMockAtualizarComic(Guid idObra, string titulo, bool falhar)
        {
            var form = new MultipartFormDataContent();
            var codigoHexa = "#81F7F3";

            if (falhar)
            {
                codigoHexa = "#81F7F";
            }

            form.Add(new StringContent(idObra.ToString()), "Id");
            form.Add(new StringContent(titulo), "Titulo");
            form.Add(new StringContent("Shadow"), "Alias");
            form.Add(new StringContent("A Eminência nas Sombras, The Eminence in Shadow, To Be a Power in The Shadow"), "TituloAlternativo");
            form.Add(new StringContent("Aizawa Daisuke"), "Autor");
            form.Add(new StringContent("SAKANO Anri"), "Artista");
            form.Add(new StringContent("2018"), "Ano");
            form.Add(new StringContent("Bravo"), "UsuarioInclusao");
            form.Add(new StringContent("NERO_SL"), "UsuarioAlteracao");
            form.Add(new StringContent("Um jovem que apenas finge ser um herói nas sombras, suas subordinadas que cometem mal-entendidos e uma poderosa organização secreta que estava no caminho…"), "Sinopse");
            form.Add(new StringContent("false"), "EhObraMaiorIdade");
            form.Add(new StringContent("fantasia,aventura,drama,seinen"), "ListaGeneros");
            form.Add(new StringContent(codigoHexa), "CodigoCorHexaObra");
            form.Add(new StringContent("japonesa"), "NacionalidadeSlug");
            form.Add(new StringContent("em-andamento"), "StatusObraSlug");
            form.Add(new StringContent("manga"), "TipoObraSlug");
            form.Add(new StringContent("false"), "EhRecomendacao");

            return form;
        }
    }
}
