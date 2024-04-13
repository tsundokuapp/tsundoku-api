using TsundokuTraducoes.Integration.Tests.Recursos;

namespace TsundokuTraducoes.Integration.Tests.Volumes
{
    public static class MockVolumeNovel
    {
        public static MultipartFormDataContent RetornaFormDataMockAdicionarVolumeNovel(bool falhar, Guid obraid)
        {
            var form = new MultipartFormDataContent();
            var numero = $"{RetornaNumeroAleatorio()}";

            if (falhar)
            {
                numero = string.Empty;
            }

            form.Add(new StringContent(""), "Titulo");
            form.Add(new StringContent(numero), "Numero");
            form.Add(new StringContent("A Bruxa, Sim, sou eu."), "Sinopse");
            form.Add(new StringContent("Bravo"), "UsuarioInclusao");
            form.Add(new StringContent(obraid.ToString()), "ObraId");
            var contentImagemVolume = MockBase.RetornaStreamImagemMock("imagemVolume.jpeg", "ImagemVolumeFile");
            form.Add(contentImagemVolume);

            return form;
        }

        public static MultipartFormDataContent RetornaFormDataMockAtualizarVolumeNovel(bool falhar, Guid obraid, Guid? idVolume)
        {
            var form = new MultipartFormDataContent();
            var numero = $"{RetornaNumeroAleatorio()}";
            var loginAlteracao = "NERO_SL";

            if (falhar)
                numero = string.Empty;

            if (falhar)
                loginAlteracao = string.Empty;

            form.Add(new StringContent(idVolume.ToString()), "Id");
            form.Add(new StringContent("Esse volume tem título"), "Titulo");
            form.Add(new StringContent(numero), "Numero");
            form.Add(new StringContent("Teste de sinopse alterada"), "Sinopse");
            form.Add(new StringContent("Bravo"), "UsuarioInclusao");
            form.Add(new StringContent(loginAlteracao), "UsuarioAlteracao");
            form.Add(new StringContent(obraid.ToString()), "ObraId");
            var contentImagemVolume = MockBase.RetornaStreamImagemMock("imagemVolume.jpeg", "ImagemVolumeFile");
            form.Add(contentImagemVolume);

            return form;
        }

        public static HttpContent RetornaFormDataMockAdicionaUmaNovel()
        {
            var form = new MultipartFormDataContent();
            var titulo = $"Novel Teste Volume - {Guid.NewGuid().ToString().Substring(0, 8)}_{Guid.NewGuid().ToString().Substring(0, 8)}";

            form.Add(new StringContent(titulo), "Titulo");
            form.Add(new StringContent(titulo), "Alias");
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

            var contentImagemPrincipal = MockBase.RetornaStreamImagemMock("imagemPrincipal.jpeg", "ImagemCapaPrincipalFile");
            var contentImagemBanner = MockBase.RetornaStreamImagemMock("imagemBanner.jpeg", "ImagemBannerFile");
            form.Add(contentImagemPrincipal);
            form.Add(contentImagemBanner);

            return form;
        }

        private static string RetornaNumeroAleatorio()
        {
            var random = new Random();
            return $"{random.Next(1, 199)}.{random.Next(1, 199)}";
        }
    }
}