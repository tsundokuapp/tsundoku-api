﻿using System.Net.Http.Headers;

namespace TsundokuTraducoes.Integration.Tests.RequestPublicas
{
    public static class MockResquestNovel
    {
        public static MultipartFormDataContent RetornaFormDataMockAdicionarNovel(string generos)
        {
            var form = new MultipartFormDataContent();
            var titulo = $"Bruxa Errante, A Jornada de Elaina - Teste - {Guid.NewGuid().ToString()[..8]}";

            form.Add(new StringContent(titulo), "Titulo");
            form.Add(new StringContent("Bruxa Errante"), "Alias");
            form.Add(new StringContent("Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々"), "TituloAlternativo");
            form.Add(new StringContent("Shiraishi Jougi"), "Autor");
            form.Add(new StringContent("Azure"), "Artista");
            form.Add(new StringContent("2017"), "Ano");
            form.Add(new StringContent("Bravo"), "UsuarioInclusao");
            form.Add(new StringContent("A Bruxa, Sim, sou eu."), "Sinopse");
            form.Add(new StringContent("false"), "EhObraMaiorIdade");
            form.Add(new StringContent(generos), "ListaGeneros");
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

        public static HttpContent RetornaStreamImagemMock(string nomeArquivo, string idForm)
        {
            var stream = new MemoryStream();

            var httpcontent = new StreamContent(stream);
            httpcontent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = idForm,
                FileName = nomeArquivo
            };

            var teste = new MediaTypeHeaderValue("image/jpg");
            httpcontent.Headers.ContentType = teste;

            return httpcontent;
        }
    }
}
