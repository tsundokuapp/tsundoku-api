using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers;

namespace TsundokuTraducoes.Tests.Entities.Obras
{
    public class ObrasTestes
    {
        #region TESTES - NOVELS

        [Fact]
        public void CriarNovelValida()
        {
            var novel = new Novel();
            novel.AdicionaNovel(Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                                  "Bruxa Errante, a Jornada de Elaina",
                                  "Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々",
                                  "Bruxa Errante",
                                  "Shiraishi Jougi",
                                  "Azure",
                                  "2017",
                                  "bruxa-errante-a-jornada-de-elaina",
                                  "Bravo",
                                  "Bravo",
                                  "https://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V8_Capa.jpg",
                                  "A Bruxa, Sim, sou eu.",
                                  DateTime.Now,
                                  DateTime.Now,
                                  false,
                                  false,
                                  "#81F7F3",
                                  "https://tsundoku.com.br/wp-content/uploads/2021/12/testeBanner.jpg",
                                  "@Bruxa Errante, a Jornada de Elaina",
                                  Diretorios.RetornaDiretorioImagemCriado("BruxaErrante"),
                                  "Em andamento",
                                  "Light Novel",
                                  "Japonesa",
                                  "Uma obra muito boa",
                                  false);

            novel.AtualizaDadosUltimoVolume("https://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg", "Volume 01", "volume-1");
            novel.AtualizaDadosUltimoCapitulo("Ilustrações", "ilustracoes", DateTime.Now);

            Assert.Equal("Azure", novel.Artista);
            Assert.Equal("bruxa-errante-a-jornada-de-elaina", novel.Slug);
            Assert.Equal("Bruxa Errante, a Jornada de Elaina", novel.Titulo);
            Assert.NotEmpty(novel.ImagemCapaPrincipal);
            Assert.NotNull(novel);
        }

        [Fact]
        public void DeveFalharAoCriarSemImagemDaCapaPrincipal()
        {
            var novel = new Novel();
            novel.AdicionaNovel(Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                                  "Bruxa Errante, a Jornada de Elaina",
                                  "Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々",
                                  "Bruxa Errante",
                                  "Shiraishi Jougi",
                                  "Azure",
                                  "2017",
                                  "bruxa-errante-a-jornada-de-elaina",
                                  "Bravo",
                                  "Bravo",
                                  "",
                                  "A Bruxa, Sim, sou eu.",
                                  DateTime.Now,
                                  DateTime.Now,
                                  false,
                                  false,
                                  "#81F7F3",
                                  "https://tsundoku.com.br/wp-content/uploads/2021/12/testeBanner.jpg",
                                  "@Bruxa Errante, a Jornada de Elaina",
                                  Diretorios.RetornaDiretorioImagemCriado("BruxaErrante"),
                                  "Em andamento",
                                  "Light Novel",
                                  "Japonesa",
                                  "Uma obra muito boa",
                                  false);

            novel.AtualizaDadosUltimoVolume("https://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg", "Volume 01", "volume-1");
            novel.AtualizaDadosUltimoCapitulo("Ilustrações", "ilustracoes", DateTime.Now);

            Assert.Empty(novel.ImagemCapaPrincipal);
        }

        [Fact]
        public void EntidadeNovel_DeveCriarUmaNovel()
        {
            var obrasRepository = new ObrasFactory();
            var novel = obrasRepository.GerarNovel();

            Assert.NotNull(novel);
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", novel.Id.ToString());
            Assert.False(string.IsNullOrEmpty(novel.Titulo));
            Assert.False(string.IsNullOrEmpty(novel.TituloAlternativo));
            Assert.False(string.IsNullOrEmpty(novel.Alias));
            Assert.False(string.IsNullOrEmpty(novel.Autor));
            Assert.False(string.IsNullOrEmpty(novel.Artista));
            Assert.False(string.IsNullOrEmpty(novel.Ano));
            Assert.False(string.IsNullOrEmpty(novel.Slug));
            Assert.False(string.IsNullOrEmpty(novel.UsuarioInclusao));
            Assert.False(string.IsNullOrEmpty(novel.UsuarioAlteracao));
            Assert.False(string.IsNullOrEmpty(novel.ImagemCapaPrincipal));
            Assert.False(string.IsNullOrEmpty(novel.Sinopse));
            Assert.NotEqual(new DateTime(), novel.DataInclusao);
            Assert.NotEqual(new DateTime(), novel.DataAlteracao);
            Assert.False(novel.EhObraMaiorIdade);
            Assert.False(novel.EhRecomendacao);
            Assert.False(string.IsNullOrEmpty(novel.CodigoCorHexaObra));
            Assert.False(string.IsNullOrEmpty(novel.ImagemBanner));
            Assert.False(string.IsNullOrEmpty(novel.CargoObraDiscord));
            Assert.False(string.IsNullOrEmpty(novel.DiretorioImagemObra));
            Assert.False(string.IsNullOrEmpty(novel.StatusObra));
            Assert.False(string.IsNullOrEmpty(novel.TipoObra));
            Assert.False(string.IsNullOrEmpty(novel.Nacionalidade));
            Assert.False(string.IsNullOrEmpty(novel.Observacao));
            Assert.False(novel.IntegracaoDiscord);
            Assert.True(string.IsNullOrEmpty(novel.ImagemCapaUltimoVolume));
            Assert.True(string.IsNullOrEmpty(novel.NumeroUltimoVolume));
            Assert.True(string.IsNullOrEmpty(novel.SlugUltimoVolume));
            Assert.True(string.IsNullOrEmpty(novel.NumeroUltimoCapitulo));
            Assert.True(string.IsNullOrEmpty(novel.SlugUltimoCapitulo));
            Assert.Null(novel.DataAtualizacaoUltimoCapitulo);

            Assert.True(novel.Volumes.Count == 0);
        }

        [Fact]
        public void EntidadeNovel_DeveCriarUmaNovelComAdicaoDeVolume()
        {
            var obrasRepository = new ObrasFactory();
            var novel = obrasRepository.GerarNovelComAdicaoVolume();

            Assert.NotNull(novel);
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", novel.Id.ToString());
            Assert.False(string.IsNullOrEmpty(novel.Titulo));
            Assert.False(string.IsNullOrEmpty(novel.TituloAlternativo));
            Assert.False(string.IsNullOrEmpty(novel.Alias));
            Assert.False(string.IsNullOrEmpty(novel.Autor));
            Assert.False(string.IsNullOrEmpty(novel.Artista));
            Assert.False(string.IsNullOrEmpty(novel.Ano));
            Assert.False(string.IsNullOrEmpty(novel.Slug));
            Assert.False(string.IsNullOrEmpty(novel.UsuarioInclusao));
            Assert.False(string.IsNullOrEmpty(novel.UsuarioAlteracao));
            Assert.False(string.IsNullOrEmpty(novel.ImagemCapaPrincipal));
            Assert.False(string.IsNullOrEmpty(novel.Sinopse));
            Assert.NotEqual(new DateTime(), novel.DataInclusao);
            Assert.NotEqual(new DateTime(), novel.DataAlteracao);
            Assert.False(novel.EhObraMaiorIdade);
            Assert.False(novel.EhRecomendacao);
            Assert.False(string.IsNullOrEmpty(novel.CodigoCorHexaObra));
            Assert.False(string.IsNullOrEmpty(novel.ImagemBanner));
            Assert.False(string.IsNullOrEmpty(novel.CargoObraDiscord));
            Assert.False(string.IsNullOrEmpty(novel.DiretorioImagemObra));
            Assert.False(string.IsNullOrEmpty(novel.StatusObra));
            Assert.False(string.IsNullOrEmpty(novel.TipoObra));
            Assert.False(string.IsNullOrEmpty(novel.Nacionalidade));
            Assert.False(string.IsNullOrEmpty(novel.Observacao));
            Assert.False(novel.IntegracaoDiscord);
            Assert.False(string.IsNullOrEmpty(novel.ImagemCapaUltimoVolume));
            Assert.False(string.IsNullOrEmpty(novel.NumeroUltimoVolume));
            Assert.False(string.IsNullOrEmpty(novel.SlugUltimoVolume));
            Assert.True(string.IsNullOrEmpty(novel.NumeroUltimoCapitulo));
            Assert.True(string.IsNullOrEmpty(novel.SlugUltimoCapitulo));
            Assert.Null(novel.DataAtualizacaoUltimoCapitulo);

            Assert.True(novel.Volumes.Count > 0);
        }

        [Fact]
        public void EntidadeNovel_DeveCriarUmaNovelComAdicaoDeCapitulo()
        {
            var obrasRepository = new ObrasFactory();
            var novel = obrasRepository.GerarNovelComAdicaoCapitulo();

            Assert.NotNull(novel);
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", novel.Id.ToString());
            Assert.False(string.IsNullOrEmpty(novel.Titulo));
            Assert.False(string.IsNullOrEmpty(novel.TituloAlternativo));
            Assert.False(string.IsNullOrEmpty(novel.Alias));
            Assert.False(string.IsNullOrEmpty(novel.Autor));
            Assert.False(string.IsNullOrEmpty(novel.Artista));
            Assert.False(string.IsNullOrEmpty(novel.Ano));
            Assert.False(string.IsNullOrEmpty(novel.Slug));
            Assert.False(string.IsNullOrEmpty(novel.UsuarioInclusao));
            Assert.False(string.IsNullOrEmpty(novel.UsuarioAlteracao));
            Assert.False(string.IsNullOrEmpty(novel.ImagemCapaPrincipal));
            Assert.False(string.IsNullOrEmpty(novel.Sinopse));
            Assert.NotEqual(new DateTime(), novel.DataInclusao);
            Assert.NotEqual(new DateTime(), novel.DataAlteracao);
            Assert.False(novel.EhObraMaiorIdade);
            Assert.False(novel.EhRecomendacao);
            Assert.False(string.IsNullOrEmpty(novel.CodigoCorHexaObra));
            Assert.False(string.IsNullOrEmpty(novel.ImagemBanner));
            Assert.False(string.IsNullOrEmpty(novel.CargoObraDiscord));
            Assert.False(string.IsNullOrEmpty(novel.DiretorioImagemObra));
            Assert.False(string.IsNullOrEmpty(novel.StatusObra));
            Assert.False(string.IsNullOrEmpty(novel.TipoObra));
            Assert.False(string.IsNullOrEmpty(novel.Nacionalidade));
            Assert.False(string.IsNullOrEmpty(novel.Observacao));
            Assert.False(novel.IntegracaoDiscord);
            Assert.False(string.IsNullOrEmpty(novel.ImagemCapaUltimoVolume));
            Assert.False(string.IsNullOrEmpty(novel.NumeroUltimoVolume));
            Assert.False(string.IsNullOrEmpty(novel.SlugUltimoVolume));
            Assert.False(string.IsNullOrEmpty(novel.NumeroUltimoCapitulo));
            Assert.False(string.IsNullOrEmpty(novel.SlugUltimoCapitulo));
            Assert.NotNull(novel.DataAtualizacaoUltimoCapitulo);

            Assert.True(novel.Volumes.Count > 0);
            Assert.True(novel.Volumes[0].ListaCapitulo.Count > 0);
        }

        #endregion

        #region TESTES - COMICS

        [Fact]
        public void CriarComicValida()
        {
            var comic = new Comic();
            comic.AdicionaComic(Guid.Parse("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                                  "Hatsukoi Losstime",
                                  "初恋ロスタイム",
                                  "Hatsukoi Losstime",
                                  "Nishina Yuuki",
                                  "Nanora & Zerokich",
                                  "2019",
                                  "hatsukoi-losstime",
                                  "Bravo",
                                  "Bravo",
                                  "https://tsundoku.com.br/wp-content/uploads/2022/01/cover_hatsukoi_vol2.jpg",
                                  "Em um mundo onde apenas duas pessoas se moviam...",
                                  DateTime.Now,
                                  DateTime.Now,
                                  false,
                                  false,
                                  "#01DFD7",
                                  "https://tsundoku.com.br/wp-content/uploads/2022/01/HatsukoiEmbed.jpg",
                                  "@Hatsukoi Losstime",
                                  Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime"),
                                  "Em andamento",
                                  "Mangá",
                                  "Japonesa",
                                  "Uma obra muito boa",
                                  false);

            comic.AtualizaDadosUltimoVolume("https://tsundoku.com.br/wp-content/uploads/2022/01/Hatsukoi_cover.jpg", "Volume 01", "volume-1");
            comic.AtualizaDadosUltimoCapitulo("Capítulo 01", "capitulo-1", DateTime.Now);

            Assert.Equal("Nishina Yuuki", comic.Autor);
            Assert.Equal("Hatsukoi Losstime", comic.Titulo);
            Assert.NotEmpty(comic.ImagemCapaPrincipal);
            Assert.NotNull(comic);
        }

        [Fact]
        public void DeveFalharAoCriaSemTitulo()
        {
            var comic = new Comic();
            comic.AdicionaComic(Guid.Parse("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                                  "",
                                  "初恋ロスタイム",
                                  "Hatsukoi Losstime",
                                  "Nishina Yuuki",
                                  "Nanora & Zerokich",
                                  "2019",
                                  "hatsukoi-losstime",
                                  "Bravo",
                                  "Bravo",
                                  "",
                                  "Em um mundo onde apenas duas pessoas se moviam...",
                                  DateTime.Now,
                                  DateTime.Now,
                                  false,
                                  false,
                                  "#01DFD7",
                                  "https://tsundoku.com.br/wp-content/uploads/2022/01/HatsukoiEmbed.jpg",
                                  "@Hatsukoi Losstime",
                                  Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime"),
                                  "Em andamento",
                                  "Mangá",
                                  "Japonesa",
                                  "Uma obra muito boa",
                                  false);

            comic.AtualizaDadosUltimoVolume("https://tsundoku.com.br/wp-content/uploads/2022/01/Hatsukoi_cover.jpg", "Volume 01", "volume-1");
            comic.AtualizaDadosUltimoCapitulo("Capítulo 01", "capitulo-1", DateTime.Now);

            Assert.Empty(comic.Titulo);
        }

        [Fact]
        public void EntidadeComic_DeveCriarUmaComic()
        {
            var obrasRepository = new ObrasFactory();
            var comic = obrasRepository.GerarComic();

            Assert.NotNull(comic);
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", comic.Id.ToString());
            Assert.False(string.IsNullOrEmpty(comic.Titulo));
            Assert.False(string.IsNullOrEmpty(comic.TituloAlternativo));
            Assert.False(string.IsNullOrEmpty(comic.Alias));
            Assert.False(string.IsNullOrEmpty(comic.Autor));
            Assert.False(string.IsNullOrEmpty(comic.Artista));
            Assert.False(string.IsNullOrEmpty(comic.Ano));
            Assert.False(string.IsNullOrEmpty(comic.Slug));
            Assert.Null(comic.Visualizacoes);
            Assert.False(string.IsNullOrEmpty(comic.UsuarioInclusao));
            Assert.False(string.IsNullOrEmpty(comic.UsuarioAlteracao));
            Assert.False(string.IsNullOrEmpty(comic.ImagemCapaPrincipal));
            Assert.False(string.IsNullOrEmpty(comic.Sinopse));
            Assert.NotEqual(new DateTime(), comic.DataInclusao);
            Assert.NotEqual(new DateTime(), comic.DataAlteracao);
            Assert.False(comic.EhObraMaiorIdade);
            Assert.False(comic.EhRecomendacao);
            Assert.False(string.IsNullOrEmpty(comic.CodigoCorHexaObra));
            Assert.False(string.IsNullOrEmpty(comic.ImagemBanner));
            Assert.False(string.IsNullOrEmpty(comic.CargoObraDiscord));
            Assert.False(string.IsNullOrEmpty(comic.DiretorioImagemObra));
            Assert.False(string.IsNullOrEmpty(comic.StatusObra));
            Assert.False(string.IsNullOrEmpty(comic.TipoObra));
            Assert.False(string.IsNullOrEmpty(comic.Nacionalidade));
            Assert.True(string.IsNullOrEmpty(comic.ImagemCapaUltimoVolume));
            Assert.True(string.IsNullOrEmpty(comic.NumeroUltimoVolume));
            Assert.True(string.IsNullOrEmpty(comic.SlugUltimoVolume));
            Assert.True(string.IsNullOrEmpty(comic.NumeroUltimoCapitulo));
            Assert.True(string.IsNullOrEmpty(comic.SlugUltimoCapitulo));
            Assert.Null(comic.DataAtualizacaoUltimoCapitulo);
            Assert.False(string.IsNullOrEmpty(comic.Observacao));
            Assert.False(comic.IntegracaoDiscord);


            Assert.True(comic.Volumes.Count == 0);
        }

        [Fact]
        public void EntidadeComic_DeveCriarUmaComicComAdicaoDeVolume()
        {
            var obrasRepository = new ObrasFactory();
            var comic = obrasRepository.GerarComicComAdicaoVolume();

            Assert.NotNull(comic);
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", comic.Id.ToString());
            Assert.False(string.IsNullOrEmpty(comic.Titulo));
            Assert.False(string.IsNullOrEmpty(comic.TituloAlternativo));
            Assert.False(string.IsNullOrEmpty(comic.Alias));
            Assert.False(string.IsNullOrEmpty(comic.Autor));
            Assert.False(string.IsNullOrEmpty(comic.Artista));
            Assert.False(string.IsNullOrEmpty(comic.Ano));
            Assert.False(string.IsNullOrEmpty(comic.Slug));
            Assert.Null(comic.Visualizacoes);
            Assert.False(string.IsNullOrEmpty(comic.UsuarioInclusao));
            Assert.False(string.IsNullOrEmpty(comic.UsuarioAlteracao));
            Assert.False(string.IsNullOrEmpty(comic.ImagemCapaPrincipal));
            Assert.False(string.IsNullOrEmpty(comic.Sinopse));
            Assert.NotEqual(new DateTime(), comic.DataInclusao);
            Assert.NotEqual(new DateTime(), comic.DataAlteracao);
            Assert.False(comic.EhObraMaiorIdade);
            Assert.False(comic.EhRecomendacao);
            Assert.False(string.IsNullOrEmpty(comic.CodigoCorHexaObra));
            Assert.False(string.IsNullOrEmpty(comic.ImagemBanner));
            Assert.False(string.IsNullOrEmpty(comic.CargoObraDiscord));
            Assert.False(string.IsNullOrEmpty(comic.DiretorioImagemObra));
            Assert.False(string.IsNullOrEmpty(comic.StatusObra));
            Assert.False(string.IsNullOrEmpty(comic.TipoObra));
            Assert.False(string.IsNullOrEmpty(comic.Nacionalidade));
            Assert.False(string.IsNullOrEmpty(comic.ImagemCapaUltimoVolume));
            Assert.False(string.IsNullOrEmpty(comic.NumeroUltimoVolume));
            Assert.False(string.IsNullOrEmpty(comic.SlugUltimoVolume));
            Assert.True(string.IsNullOrEmpty(comic.NumeroUltimoCapitulo));
            Assert.True(string.IsNullOrEmpty(comic.SlugUltimoCapitulo));
            Assert.Null(comic.DataAtualizacaoUltimoCapitulo);
            Assert.False(string.IsNullOrEmpty(comic.Observacao));
            Assert.False(comic.IntegracaoDiscord);

            Assert.True(comic.Volumes.Count > 0);
        }

        [Fact]
        public void EntidadeComic_DeveCriarUmaComicComAdicaoDeCapitulo()
        {
            var obrasRepository = new ObrasFactory();
            var comic = obrasRepository.GerarComicComAdicaoCapitulo();

            Assert.NotNull(comic);
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", comic.Id.ToString());
            Assert.False(string.IsNullOrEmpty(comic.Titulo));
            Assert.False(string.IsNullOrEmpty(comic.TituloAlternativo));
            Assert.False(string.IsNullOrEmpty(comic.Alias));
            Assert.False(string.IsNullOrEmpty(comic.Autor));
            Assert.False(string.IsNullOrEmpty(comic.Artista));
            Assert.False(string.IsNullOrEmpty(comic.Ano));
            Assert.False(string.IsNullOrEmpty(comic.Slug));
            Assert.Null(comic.Visualizacoes);
            Assert.False(string.IsNullOrEmpty(comic.UsuarioInclusao));
            Assert.False(string.IsNullOrEmpty(comic.UsuarioAlteracao));
            Assert.False(string.IsNullOrEmpty(comic.ImagemCapaPrincipal));
            Assert.False(string.IsNullOrEmpty(comic.Sinopse));
            Assert.NotEqual(new DateTime(), comic.DataInclusao);
            Assert.NotEqual(new DateTime(), comic.DataAlteracao);
            Assert.False(comic.EhObraMaiorIdade);
            Assert.False(comic.EhRecomendacao);
            Assert.False(string.IsNullOrEmpty(comic.CodigoCorHexaObra));
            Assert.False(string.IsNullOrEmpty(comic.ImagemBanner));
            Assert.False(string.IsNullOrEmpty(comic.CargoObraDiscord));
            Assert.False(string.IsNullOrEmpty(comic.DiretorioImagemObra));
            Assert.False(string.IsNullOrEmpty(comic.StatusObra));
            Assert.False(string.IsNullOrEmpty(comic.TipoObra));
            Assert.False(string.IsNullOrEmpty(comic.Nacionalidade));
            Assert.False(string.IsNullOrEmpty(comic.ImagemCapaUltimoVolume));
            Assert.False(string.IsNullOrEmpty(comic.NumeroUltimoVolume));
            Assert.False(string.IsNullOrEmpty(comic.SlugUltimoVolume));
            Assert.False(string.IsNullOrEmpty(comic.NumeroUltimoCapitulo));
            Assert.False(string.IsNullOrEmpty(comic.SlugUltimoCapitulo));
            Assert.NotNull(comic.DataAtualizacaoUltimoCapitulo);
            Assert.False(string.IsNullOrEmpty(comic.Observacao));
            Assert.False(comic.IntegracaoDiscord);

            Assert.True(comic.Volumes.Count > 0);
            Assert.True(comic.Volumes[0].ListaCapitulo.Count > 0);
            Assert.True(comic.Volumes[0].ListaCapitulo[0].ListaImagensJson.Length > 0);
        }

        #endregion
    }
}