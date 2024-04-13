using TsundokuTraducoes.Integration.Tests.Recursos;

namespace TsundokuTraducoes.Integration.Tests.Volumes
{
    public static class MockVolumeComic
    {
        public static MultipartFormDataContent RetornaFormDataMockAdicionarVolumeComic(bool falhar, Guid obraId)
        {
            var form = new MultipartFormDataContent();
            var numero = $"{RetornaNumeroAleatorio()}";

            if (falhar)
            {
                numero = string.Empty;
            }

            form.Add(new StringContent(""), "Titulo");
            form.Add(new StringContent(numero), "Numero");
            form.Add(new StringContent("O mestre"), "Sinopse");
            form.Add(new StringContent("Bravo"), "UsuarioInclusao");
            form.Add(new StringContent(obraId.ToString()), "ObraId");
            var contentImagemVolume = MockBase.RetornaStreamImagemMock("imagemVolume.jpeg", "ImagemVolumeFile");
            form.Add(contentImagemVolume);

            return form;
        }

        public static MultipartFormDataContent RetornaFormDataMockAtualizarVolumeComic(bool falhar, Guid obraid, Guid? idVolume)
        {
            var form = new MultipartFormDataContent();
            var numero = $"{RetornaNumeroAleatorio()}";
            var loginAlteracao = "Araragui";

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

        public static HttpContent RetornaFormDataMockAdicionaComic()
        {
            var form = new MultipartFormDataContent();
            var titulo = $"Comic Teste Volume - {Guid.NewGuid().ToString().Substring(0, 8)}_{Guid.NewGuid().ToString().Substring(0, 8)}";

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
