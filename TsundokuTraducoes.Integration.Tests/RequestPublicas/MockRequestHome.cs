using System.Net.Http.Headers;

namespace TsundokuTraducoes.Integration.Tests.RequestPublicas
{
    public class MockRequestHome
    {
        public static MultipartFormDataContent RetornaFormDataMockAdicionaComic()
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

            var contentImagemPrincipal = RetornaStreamImagemMock("imagemPrincipal.jpeg", "ImagemCapaPrincipalFile");
            var contentImagemBanner = RetornaStreamImagemMock("imagemBanner.jpeg", "ImagemBannerFile");
            form.Add(contentImagemPrincipal);
            form.Add(contentImagemBanner);

            return form;
        }

        public static MultipartFormDataContent RetornaFormDataMockAdicionarVolumeComic(Guid obraId)
        {
            var form = new MultipartFormDataContent();
            var numero = $"{RetornaNumeroAleatorio()}";

            form.Add(new StringContent(""), "Titulo");
            form.Add(new StringContent(numero), "Numero");
            form.Add(new StringContent("O mestre"), "Sinopse");
            form.Add(new StringContent("Bravo"), "UsuarioInclusao");
            form.Add(new StringContent(obraId.ToString()), "ObraId");
            var contentImagemVolume = RetornaStreamImagemMock("imagemVolume.jpeg", "ImagemVolumeFile");
            form.Add(contentImagemVolume);

            return form;
        }

        public static MultipartFormDataContent RetornaFormDataMockAdicionarCapituloComic(Guid volumeId)
        {
            var form = new MultipartFormDataContent();
            var numero = $"{RetornaNumeroAleatorio()}";

            form.Add(new StringContent(numero), "Numero");
            form.Add(new StringContent(""), "Parte");
            form.Add(new StringContent("O mestre"), "Titulo");
            form.Add(new StringContent("Bravo"), "UsuarioInclusao");
            form.Add(new StringContent(volumeId.ToString()), "VolumeId");
            form.Add(new StringContent("1"), "OrdemCapitulo");

            var listaContentImagemCapitulo = new List<HttpContent>();

            for (int i = 0; i < 10; i++)
            {
                listaContentImagemCapitulo.Add(RetornaStreamImagemMock($"pagina{i:00}.jpeg", "ListaImagensForm"));
            }

            foreach (var contentImagemCapitulo in listaContentImagemCapitulo)
            {
                form.Add(contentImagemCapitulo);
            }

            return form;
        }


        public static MultipartFormDataContent RetornaFormDataMockAdicionaNovel()
        {
            var form = new MultipartFormDataContent();
            var titulo = $"Novel Teste Capitulo - {Guid.NewGuid().ToString().Substring(0, 8)}_{Guid.NewGuid().ToString().Substring(0, 8)}";

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

            var contentImagemPrincipal = RetornaStreamImagemMock("imagemPrincipal.jpeg", "ImagemCapaPrincipalFile");
            var contentImagemBanner = RetornaStreamImagemMock("imagemBanner.jpeg", "ImagemBannerFile");
            form.Add(contentImagemPrincipal);
            form.Add(contentImagemBanner);

            return form;
        }

        public static MultipartFormDataContent RetornaFormDataMockAdicionarVolumeNovel(Guid obraId)
        {
            var form = new MultipartFormDataContent();
            var numero = $"{RetornaNumeroAleatorio()}";

            form.Add(new StringContent(""), "Titulo");
            form.Add(new StringContent(numero), "Numero");
            form.Add(new StringContent("O mestre"), "Sinopse");
            form.Add(new StringContent("Bravo"), "UsuarioInclusao");
            form.Add(new StringContent(obraId.ToString()), "ObraId");
            var contentImagemVolume = RetornaStreamImagemMock("imagemVolume.jpeg", "ImagemVolumeFile");
            form.Add(contentImagemVolume);

            return form;
        }

        public static MultipartFormDataContent RetornaFormDataMockAdicionarCapituloNovel(Guid volumeId)
        {
            var form = new MultipartFormDataContent();
            var numero = $"{RetornaNumeroAleatorio()}";
            var titulo = $"O Destino de Alguns Aventureiros - Teste - {Guid.NewGuid().ToString().Substring(0, 8)}";
            var conteudoCapitulo = RetornoConteudoCapituloNovel();          

            form.Add(new StringContent(numero), "Numero");
            form.Add(new StringContent(""), "Parte");
            form.Add(new StringContent(titulo), "Titulo");
            form.Add(new StringContent(conteudoCapitulo), "ConteudoNovel");
            form.Add(new StringContent("Bravo"), "UsuarioInclusao");
            form.Add(new StringContent(volumeId.ToString()), "VolumeId");
            form.Add(new StringContent("Equipe 3Lobos"), "Tradutor");
            form.Add(new StringContent("Equipe 3Lobos"), "Revisor");
            form.Add(new StringContent(""), "QC");
            form.Add(new StringContent("Equipe Tsundoku"), "Editores");
            form.Add(new StringContent("2"), "OrdemCapitulo");
            form.Add(new StringContent("false"), "EhIlustracoesNovel");

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

        private static string RetornaNumeroAleatorio()
        {
            var random = new Random();
            return $"{random.Next(1, 199)}.{random.Next(1, 199)}";
        }

        private static string RetornoConteudoCapituloNovel()
        {
            return @"A luta brutal terminou, ele pisou com sua bota no cadáver do goblin morto.
                    Ele estava manchado com o sangue carmesim do monstro, do seu elmo de aço sujo e armadura de couro, até a malha feita de anéis metálicos encadeados que cobriam todo o seu corpo.
                    Um pequeno escudo surrado estava fixado em seu braço esquerdo, e em sua mão, segurava uma tocha ardente.
                    Com seu calcanhar contra o cadáver da criatura, abaixou sua mão livre e retirou casualmente a espada de seu crânio. Era uma lâmina de aparência barata, com um comprimento mal concebido, e agora estava encharcada de cérebro de goblin.
                    Deitada no chão com uma flecha no ombro, o corpo magro de uma menina tremia de medo. Seu clássico rosto adorável e doce, emoldurado pelos longos e quase translúcidos cabelos cor de ouro, estava franzido com uma junção de lagrimas e suor.
                    Seus braços magros, seus pés; todo seu corpo deslumbrante estava com as vestimentas de uma sacerdotisa. O som do cajado de monge que ela agarrou ressoou, com seus anéis pendurados batendo uns contra os outros com suas mãos trêmulas.
                    Quem era esse homem diante dela?
                    Tão estranha era sua aparência, a aura que o ocultava, no qual ela imaginava ele poder ser mesmo um goblin; ou talvez algo muito pior, algo que ela ainda não tinha conhecimento.
                    — Que-quem é você…? — perguntou, suprimindo seu terror e dor.
                    Após uma pausa, o homem respondeu: — Matador de Goblins.
                    Um assassino. Não de dragões ou de vampiros, mas o mais simples dos monstros: goblins.
                    Normalmente, o nome poderia parecer comicamente simples. Mas para Sacerdotisa, naquele momento, era tudo, menos engraçado.";
        }
    }
}