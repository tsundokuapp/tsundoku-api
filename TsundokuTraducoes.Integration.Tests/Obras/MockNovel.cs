using System.Net.Http.Headers;

namespace TsundokuTraducoes.Integration.Tests.Obras
{
    public class MockNovel : AppIntegrationBase
    {
        public MultipartFormDataContent RetornaFormDataMockAdicionarNovel(bool falhar)
        {
            var form = new MultipartFormDataContent();
            var titulo = $"Bruxa Errante, A Jornada de Elaina - Teste - {Guid.NewGuid().ToString().Substring(0, 8)}";

            if (falhar)
            {
                titulo = string.Empty;
            }
            
            form.Add(new StringContent(titulo), "Titulo");
            form.Add(new StringContent("Bruxa Errante"), "Alias");
            form.Add(new StringContent("Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々"), "TituloAlternativo");
            form.Add(new StringContent("Shiraishi Jougi"), "Autor");
            form.Add(new StringContent("Azure"), "Artista");
            form.Add(new StringContent("2017"), "Ano");
            form.Add(new StringContent("Bravo"), "UsuarioInclusao");
            form.Add(new StringContent("A Bruxa, Sim, sou eu."), "Sinopse");
            form.Add(new StringContent("false"), "EhObraMaiorIdade");
            form.Add(new StringContent("fantasia,aventura,drama"), "ListaGeneros");
            form.Add(new StringContent("#81F7F3"), "CodigoCorHexaObra");
            form.Add(new StringContent("japonesa"), "NacionalidadeSlug");
            form.Add(new StringContent("em-andamento"), "StatusObraSlug");
            form.Add(new StringContent("light-novel"), "TipoObraSlug");
            form.Add(new StringContent("false"), "EhRecomendacao");
            
            var contentImagemPrincipal = RetornaStreamImagemMock("imagemPrincipal.jpeg", "ImagemCapaPrincipalFile");
            var contentImagemBanner = RetornaStreamImagemMock("imagemBanner.jpeg", "ImagemBannerFile");
            form.Add(contentImagemPrincipal);
            form.Add(contentImagemBanner);

            return form;
        }

        public MultipartFormDataContent RetornaFormDataMockAtualizarNovel(Guid idObra, string titulo, bool falhar)
        {
            var form = new MultipartFormDataContent();
            var codigoHexa = "#81F7F3";

            if (falhar)
            {
                codigoHexa = "#81F7F";
            }

            form.Add(new StringContent(idObra.ToString()), "Id");
            form.Add(new StringContent(titulo), "Titulo");
            form.Add(new StringContent("Bruxa Errante"), "Alias");
            form.Add(new StringContent("Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々"), "TituloAlternativo");
            form.Add(new StringContent("Shiraishi Jougi"), "Autor");
            form.Add(new StringContent("Azure"), "Artista");
            form.Add(new StringContent("2016"), "Ano");
            form.Add(new StringContent("Bravo"), "UsuarioInclusao");
            form.Add(new StringContent("Axios"), "UsuarioAlteracao");
            form.Add(new StringContent("A Bruxa, Sim, sou eu. Sim é ela"), "Sinopse");
            form.Add(new StringContent("false"), "EhObraMaiorIdade");
            form.Add(new StringContent("fantasia,aventura,drama,seinen"), "ListaGeneros");
            form.Add(new StringContent(codigoHexa), "CodigoCorHexaObra");
            form.Add(new StringContent("japonesa"), "NacionalidadeSlug");
            form.Add(new StringContent("em-andamento"), "StatusObraSlug");
            form.Add(new StringContent("light-novel"), "TipoObraSlug");
            form.Add(new StringContent("false"), "EhRecomendacao");            

            return form;
        }
    }
}
