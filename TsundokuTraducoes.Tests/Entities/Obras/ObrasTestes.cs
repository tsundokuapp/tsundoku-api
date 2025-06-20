using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers;

namespace TsundokuTraducoes.Tests.Entities.Obras
{
    public class ObrasTestes
    {
        #region => TESTES - NOVELS

        [Fact]
        public void EntidadeNovel_DeveCriarUmaNovel()
        {
            var titulo = "Bruxa Errante, a Jornada dos Testes";
            var tituloAlternativo = "Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々";
            var alias = "Bruxa Errante";
            var autor = "Shiraishi Jougi";
            var artista = "Azure";
            var ano = "2017";
            var slug = "bruxa-errante-a-jornada-dos-testes";
            var usuarioInclusao = "Bravo";
            var usuarioAlteracao = "Bravo";
            var imagemCapaPrincipal = "https://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V8_Capa.jpg";
            var sinopse = "A Bruxa, Sim, sou eu.";
            var imagemBanner = "https://tsundoku.com.br/wp-content/uploads/2021/12/testeBanner.jpg";
            var status = "Em Andamento";
            var tipo = "Light Novel";
            var nacionalidade = "Japonesa";
            var observacao = "Uma obra muito boa";

            var obrasRepository = new ObrasFactory();
            var novel = obrasRepository
                .GerarNovel(titulo, tituloAlternativo, alias, autor, artista, ano, slug, 
                    usuarioInclusao, usuarioAlteracao, imagemCapaPrincipal, sinopse, 
                    imagemBanner, status, tipo, nacionalidade, observacao);

            Assert.NotNull(novel);
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", novel.Id.ToString());
            Assert.False(string.IsNullOrEmpty(novel.Titulo));
            Assert.Equal(titulo, novel.Titulo);            
            Assert.False(string.IsNullOrEmpty(novel.TituloAlternativo));
            Assert.Equal(tituloAlternativo, novel.TituloAlternativo);            
            Assert.False(string.IsNullOrEmpty(novel.Alias));
            Assert.Equal(alias, novel.Alias);            
            Assert.False(string.IsNullOrEmpty(novel.Autor));
            Assert.Equal(autor, novel.Autor);            
            Assert.False(string.IsNullOrEmpty(novel.Artista));
            Assert.Equal(artista, novel.Artista);            
            Assert.False(string.IsNullOrEmpty(novel.Ano));
            Assert.Equal(ano, novel.Ano);            
            Assert.False(string.IsNullOrEmpty(novel.Slug));
            Assert.Equal(slug, novel.Slug);
            Assert.Null(novel.Visualizacoes);
            Assert.False(string.IsNullOrEmpty(novel.UsuarioInclusao));
            Assert.Equal(usuarioInclusao, novel.UsuarioInclusao);            
            Assert.False(string.IsNullOrEmpty(novel.UsuarioAlteracao));
            Assert.Equal(usuarioAlteracao, novel.UsuarioAlteracao);          
            Assert.False(string.IsNullOrEmpty(novel.ImagemCapaPrincipal));
            Assert.Equal(imagemCapaPrincipal, novel.ImagemCapaPrincipal);            
            Assert.False(string.IsNullOrEmpty(novel.Sinopse));
            Assert.Equal(sinopse, novel.Sinopse);            
            Assert.NotEqual(new DateTime(), novel.DataInclusao);
            Assert.NotEqual(new DateTime(), novel.DataAlteracao);
            Assert.False(novel.EhObraMaiorIdade);
            Assert.False(novel.EhRecomendacao);
            Assert.False(string.IsNullOrEmpty(novel.CodigoCorHexaObra));           
            Assert.False(string.IsNullOrEmpty(novel.ImagemBanner));
            Assert.Equal(imagemBanner, novel.ImagemBanner);            
            Assert.False(string.IsNullOrEmpty(novel.CargoObraDiscord));
            Assert.False(string.IsNullOrEmpty(novel.DiretorioImagemObra));            
            Assert.False(string.IsNullOrEmpty(novel.StatusObra));
            Assert.Equal(status, novel.StatusObra);            
            Assert.False(string.IsNullOrEmpty(novel.TipoObra));
            Assert.Equal(tipo, novel.TipoObra);            
            Assert.False(string.IsNullOrEmpty(novel.Nacionalidade));
            Assert.Equal(nacionalidade, novel.Nacionalidade);            
            Assert.False(string.IsNullOrEmpty(novel.Observacao));
            Assert.Equal(observacao, novel.Observacao);            
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
            var titulo = "Bruxa Errante, a Jornada dos Testes";
            var tituloAlternativo = "Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々";
            var alias = "Bruxa Errante";
            var autor = "Shiraishi Jougi";
            var artista = "Azure";
            var ano = "2017";
            var slug = "bruxa-errante-a-jornada-dos-testes";
            var usuarioInclusao = "Bravo";
            var usuarioAlteracao = "Bravo";
            var imagemCapaPrincipal = "https://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V8_Capa.jpg";
            var sinopse = "A Bruxa, Sim, sou eu.";
            var imagemBanner = "https://tsundoku.com.br/wp-content/uploads/2021/12/testeBanner.jpg";
            var status = "Em Andamento";
            var tipo = "Light Novel";
            var nacionalidade = "Japonesa";
            var observacao = "Uma obra muito boa";
            var imagemUltimoVolume = "https://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg";
            var numeroUltimoVolume = "Volume 01";
            var slugUltimoVolume = "volume-1";

            var obrasRepository = new ObrasFactory();
            var novel = obrasRepository
                .GerarNovelComAdicaoVolume(titulo, tituloAlternativo, alias, autor, artista, ano, slug,
                    usuarioInclusao, usuarioAlteracao, imagemCapaPrincipal, sinopse,
                    imagemBanner, status, tipo, nacionalidade, observacao, imagemUltimoVolume, numeroUltimoVolume, slugUltimoVolume);

            Assert.NotNull(novel);
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", novel.Id.ToString());
            Assert.False(string.IsNullOrEmpty(novel.Titulo));
            Assert.Equal(titulo, novel.Titulo);
            Assert.False(string.IsNullOrEmpty(novel.TituloAlternativo));
            Assert.Equal(tituloAlternativo, novel.TituloAlternativo);
            Assert.False(string.IsNullOrEmpty(novel.Alias));
            Assert.Equal(alias, novel.Alias);
            Assert.False(string.IsNullOrEmpty(novel.Autor));
            Assert.Equal(autor, novel.Autor);
            Assert.False(string.IsNullOrEmpty(novel.Artista));
            Assert.Equal(artista, novel.Artista);
            Assert.False(string.IsNullOrEmpty(novel.Ano));
            Assert.Equal(ano, novel.Ano);
            Assert.False(string.IsNullOrEmpty(novel.Slug));
            Assert.Equal(slug, novel.Slug);
            Assert.Null(novel.Visualizacoes);
            Assert.False(string.IsNullOrEmpty(novel.UsuarioInclusao));
            Assert.Equal(usuarioInclusao, novel.UsuarioInclusao);
            Assert.False(string.IsNullOrEmpty(novel.UsuarioAlteracao));
            Assert.Equal(usuarioAlteracao, novel.UsuarioAlteracao);
            Assert.False(string.IsNullOrEmpty(novel.ImagemCapaPrincipal));
            Assert.Equal(imagemCapaPrincipal, novel.ImagemCapaPrincipal);
            Assert.False(string.IsNullOrEmpty(novel.Sinopse));
            Assert.Equal(sinopse, novel.Sinopse);
            Assert.NotEqual(new DateTime(), novel.DataInclusao);
            Assert.NotEqual(new DateTime(), novel.DataAlteracao);
            Assert.False(novel.EhObraMaiorIdade);
            Assert.False(novel.EhRecomendacao);
            Assert.False(string.IsNullOrEmpty(novel.CodigoCorHexaObra));
            Assert.False(string.IsNullOrEmpty(novel.ImagemBanner));
            Assert.Equal(imagemBanner, novel.ImagemBanner);
            Assert.False(string.IsNullOrEmpty(novel.CargoObraDiscord));
            Assert.False(string.IsNullOrEmpty(novel.DiretorioImagemObra));
            Assert.False(string.IsNullOrEmpty(novel.StatusObra));
            Assert.Equal(status, novel.StatusObra);
            Assert.False(string.IsNullOrEmpty(novel.TipoObra));
            Assert.Equal(tipo, novel.TipoObra);
            Assert.False(string.IsNullOrEmpty(novel.Nacionalidade));
            Assert.Equal(nacionalidade, novel.Nacionalidade);
            Assert.False(string.IsNullOrEmpty(novel.Observacao));
            Assert.Equal(observacao, novel.Observacao);
            Assert.False(novel.IntegracaoDiscord);
            Assert.False(string.IsNullOrEmpty(novel.ImagemCapaUltimoVolume));
            Assert.Equal(imagemUltimoVolume, novel.ImagemCapaUltimoVolume);
            Assert.False(string.IsNullOrEmpty(novel.NumeroUltimoVolume));
            Assert.Equal(numeroUltimoVolume, novel.NumeroUltimoVolume);
            Assert.False(string.IsNullOrEmpty(novel.SlugUltimoVolume));
            Assert.Equal(slugUltimoVolume, novel.SlugUltimoVolume);
            Assert.True(string.IsNullOrEmpty(novel.NumeroUltimoCapitulo));
            Assert.True(string.IsNullOrEmpty(novel.SlugUltimoCapitulo));
            Assert.Null(novel.DataAtualizacaoUltimoCapitulo);

            Assert.True(novel.Volumes.Count > 0);
        }

        [Fact]
        public void EntidadeNovel_DeveCriarUmaNovelComAdicaoDeCapitulo()
        {
            var titulo = "Bruxa Errante, a Jornada dos Testes";
            var tituloAlternativo = "Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々";
            var alias = "Bruxa Errante";
            var autor = "Shiraishi Jougi";
            var artista = "Azure";
            var ano = "2017";
            var slug = "bruxa-errante-a-jornada-dos-testes";
            var usuarioInclusao = "Bravo";
            var usuarioAlteracao = "Bravo";
            var imagemCapaPrincipal = "https://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V8_Capa.jpg";
            var sinopse = "A Bruxa, Sim, sou eu.";
            var imagemBanner = "https://tsundoku.com.br/wp-content/uploads/2021/12/testeBanner.jpg";
            var status = "Em Andamento";
            var tipo = "Light Novel";
            var nacionalidade = "Japonesa";
            var observacao = "Uma obra muito boa";
            var imagemUltimoVolume = "https://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg";
            var numeroUltimoVolume = "Volume 01";
            var slugUltimoVolume = "volume-1";
            var numeroUltimoCapitulo = "Ilustrações";
            var slugUltimoCapitulo = "ilustracoes";
            var dataAtualizacaoUltimoCapitulo = new DateTime(2025, 02, 13);

            var obrasRepository = new ObrasFactory();
            var novel = obrasRepository
                .GerarNovelComAdicaoCapitulo(titulo, tituloAlternativo, alias, autor, artista, ano, slug, usuarioInclusao, usuarioAlteracao, 
                    imagemCapaPrincipal, sinopse,imagemBanner, status, tipo, nacionalidade, observacao, imagemUltimoVolume, 
                    numeroUltimoVolume, slugUltimoVolume, numeroUltimoCapitulo, slugUltimoCapitulo, dataAtualizacaoUltimoCapitulo);

            Assert.NotNull(novel);
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", novel.Id.ToString());
            Assert.False(string.IsNullOrEmpty(novel.Titulo));
            Assert.Equal(titulo, novel.Titulo);
            Assert.False(string.IsNullOrEmpty(novel.TituloAlternativo));
            Assert.Equal(tituloAlternativo, novel.TituloAlternativo);
            Assert.False(string.IsNullOrEmpty(novel.Alias));
            Assert.Equal(alias, novel.Alias);
            Assert.False(string.IsNullOrEmpty(novel.Autor));
            Assert.Equal(autor, novel.Autor);
            Assert.False(string.IsNullOrEmpty(novel.Artista));
            Assert.Equal(artista, novel.Artista);
            Assert.False(string.IsNullOrEmpty(novel.Ano));
            Assert.Equal(ano, novel.Ano);
            Assert.False(string.IsNullOrEmpty(novel.Slug));
            Assert.Equal(slug, novel.Slug);
            Assert.Null(novel.Visualizacoes);
            Assert.False(string.IsNullOrEmpty(novel.UsuarioInclusao));
            Assert.Equal(usuarioInclusao, novel.UsuarioInclusao);
            Assert.False(string.IsNullOrEmpty(novel.UsuarioAlteracao));
            Assert.Equal(usuarioAlteracao, novel.UsuarioAlteracao);
            Assert.False(string.IsNullOrEmpty(novel.ImagemCapaPrincipal));
            Assert.Equal(imagemCapaPrincipal, novel.ImagemCapaPrincipal);
            Assert.False(string.IsNullOrEmpty(novel.Sinopse));
            Assert.Equal(sinopse, novel.Sinopse);
            Assert.NotEqual(new DateTime(), novel.DataInclusao);
            Assert.NotEqual(new DateTime(), novel.DataAlteracao);
            Assert.False(novel.EhObraMaiorIdade);
            Assert.False(novel.EhRecomendacao);
            Assert.False(string.IsNullOrEmpty(novel.CodigoCorHexaObra));
            Assert.False(string.IsNullOrEmpty(novel.ImagemBanner));
            Assert.Equal(imagemBanner, novel.ImagemBanner);
            Assert.False(string.IsNullOrEmpty(novel.CargoObraDiscord));
            Assert.False(string.IsNullOrEmpty(novel.DiretorioImagemObra));
            Assert.False(string.IsNullOrEmpty(novel.StatusObra));
            Assert.Equal(status, novel.StatusObra);
            Assert.False(string.IsNullOrEmpty(novel.TipoObra));
            Assert.Equal(tipo, novel.TipoObra);
            Assert.False(string.IsNullOrEmpty(novel.Nacionalidade));
            Assert.Equal(nacionalidade, novel.Nacionalidade);
            Assert.False(string.IsNullOrEmpty(novel.Observacao));
            Assert.Equal(observacao, novel.Observacao);
            Assert.False(novel.IntegracaoDiscord);
            Assert.False(string.IsNullOrEmpty(novel.ImagemCapaUltimoVolume));
            Assert.Equal(imagemUltimoVolume, novel.ImagemCapaUltimoVolume);
            Assert.False(string.IsNullOrEmpty(novel.NumeroUltimoVolume));
            Assert.Equal(numeroUltimoVolume, novel.NumeroUltimoVolume);
            Assert.False(string.IsNullOrEmpty(novel.SlugUltimoVolume));
            Assert.Equal(slugUltimoVolume, novel.SlugUltimoVolume);
            Assert.False(string.IsNullOrEmpty(novel.NumeroUltimoCapitulo));
            Assert.Equal(numeroUltimoCapitulo, novel.NumeroUltimoCapitulo);
            Assert.False(string.IsNullOrEmpty(novel.SlugUltimoCapitulo));
            Assert.Equal(slugUltimoCapitulo, novel.SlugUltimoCapitulo);
            Assert.NotNull(novel.DataAtualizacaoUltimoCapitulo);
            Assert.Equal(dataAtualizacaoUltimoCapitulo, novel.DataAtualizacaoUltimoCapitulo);

            Assert.True(novel.Volumes.Count > 0);
            Assert.True(novel.Volumes[0].ListaCapitulo.Count > 0);
        }

        #endregion

        #region => TESTES - COMICS

        [Fact]
        public void EntidadeComic_DeveCriarUmaComic()
        {
            var titulo = "Hatsukoi Losstime";
            var tituloAlternativo = "初恋ロスタイム";
            var alias = "Hatsukoi Losstime";
            var autor = "Nishina Yuuki";
            var artista = "Nanora & Zerokich";
            var ano = "2019";
            var slug = "hatsukoi-losstime";
            var usuarioInclusao = "Bravo";
            var usuarioAlteracao = "Bravo";
            var imagemCapaPrincipal = "https://tsundoku.com.br/wp-content/uploads/2022/01/cover_hatsukoi_vol2.jpg";
            var sinopse = "Em um mundo onde apenas duas pessoas se moviam...";
            var imagemBanner = "https://tsundoku.com.br/wp-content/uploads/2022/01/HatsukoiEmbed.jpg";
            var status = "Em andamento";
            var tipo = "Mangá";
            var nacionalidade = "Japonesa";
            var observacao = "Uma obra muito boa";

            var obrasRepository = new ObrasFactory();
            var comic = obrasRepository
                .GerarComic(titulo, tituloAlternativo, alias, autor, artista, ano, slug,
                    usuarioInclusao, usuarioAlteracao, imagemCapaPrincipal, sinopse,
                    imagemBanner, status, tipo, nacionalidade, observacao);

            Assert.NotNull(comic);
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", comic.Id.ToString());
            Assert.False(string.IsNullOrEmpty(comic.Titulo));
            Assert.Equal(titulo, comic.Titulo);
            Assert.False(string.IsNullOrEmpty(comic.TituloAlternativo));
            Assert.Equal(tituloAlternativo, comic.TituloAlternativo);
            Assert.False(string.IsNullOrEmpty(comic.Alias));
            Assert.Equal(alias, comic.Alias);
            Assert.False(string.IsNullOrEmpty(comic.Autor));
            Assert.Equal(autor, comic.Autor);
            Assert.False(string.IsNullOrEmpty(comic.Artista));
            Assert.Equal(artista, comic.Artista);
            Assert.False(string.IsNullOrEmpty(comic.Ano));
            Assert.Equal(ano, comic.Ano);
            Assert.False(string.IsNullOrEmpty(comic.Slug));
            Assert.Equal(slug, comic.Slug);
            Assert.Null(comic.Visualizacoes);
            Assert.False(string.IsNullOrEmpty(comic.UsuarioInclusao));
            Assert.Equal(usuarioInclusao, comic.UsuarioInclusao);
            Assert.False(string.IsNullOrEmpty(comic.UsuarioAlteracao));
            Assert.Equal(usuarioAlteracao, comic.UsuarioAlteracao);
            Assert.False(string.IsNullOrEmpty(comic.ImagemCapaPrincipal));
            Assert.Equal(imagemCapaPrincipal, comic.ImagemCapaPrincipal);
            Assert.False(string.IsNullOrEmpty(comic.Sinopse));
            Assert.Equal(sinopse, comic.Sinopse);
            Assert.NotEqual(new DateTime(), comic.DataInclusao);
            Assert.NotEqual(new DateTime(), comic.DataAlteracao);
            Assert.False(comic.EhObraMaiorIdade);
            Assert.False(comic.EhRecomendacao);
            Assert.False(string.IsNullOrEmpty(comic.CodigoCorHexaObra));
            Assert.False(string.IsNullOrEmpty(comic.ImagemBanner));
            Assert.Equal(imagemBanner, comic.ImagemBanner);
            Assert.False(string.IsNullOrEmpty(comic.CargoObraDiscord));
            Assert.False(string.IsNullOrEmpty(comic.DiretorioImagemObra));
            Assert.False(string.IsNullOrEmpty(comic.StatusObra));
            Assert.Equal(status, comic.StatusObra);
            Assert.False(string.IsNullOrEmpty(comic.TipoObra));
            Assert.Equal(tipo, comic.TipoObra);
            Assert.False(string.IsNullOrEmpty(comic.Nacionalidade));
            Assert.Equal(nacionalidade, comic.Nacionalidade);
            Assert.False(string.IsNullOrEmpty(comic.Observacao));
            Assert.Equal(observacao, comic.Observacao);
            Assert.False(comic.IntegracaoDiscord);
            Assert.True(string.IsNullOrEmpty(comic.ImagemCapaUltimoVolume));
            Assert.True(string.IsNullOrEmpty(comic.NumeroUltimoVolume));
            Assert.True(string.IsNullOrEmpty(comic.SlugUltimoVolume));
            Assert.True(string.IsNullOrEmpty(comic.NumeroUltimoCapitulo));
            Assert.True(string.IsNullOrEmpty(comic.SlugUltimoCapitulo));
            Assert.Null(comic.DataAtualizacaoUltimoCapitulo);            
            Assert.True(comic.Volumes.Count == 0);
        }

        [Fact]
        public void EntidadeComic_DeveCriarUmaComicComAdicaoDeVolume()
        {
            var titulo = "Hatsukoi Losstime";
            var tituloAlternativo = "初恋ロスタイム";
            var alias = "Hatsukoi Losstime";
            var autor = "Nishina Yuuki";
            var artista = "Nanora & Zerokich";
            var ano = "2019";
            var slug = "hatsukoi-losstime";
            var usuarioInclusao = "Bravo";
            var usuarioAlteracao = "Bravo";
            var imagemCapaPrincipal = "https://tsundoku.com.br/wp-content/uploads/2022/01/cover_hatsukoi_vol2.jpg";
            var sinopse = "Em um mundo onde apenas duas pessoas se moviam...";
            var imagemBanner = "https://tsundoku.com.br/wp-content/uploads/2022/01/HatsukoiEmbed.jpg";
            var status = "Em andamento";
            var tipo = "Mangá";
            var nacionalidade = "Japonesa";
            var observacao = "Uma obra muito boa";
            var imagemUltimoVolume = "https://tsundoku.com.br/wp-content/uploads/2022/01/Hatsukoi_cover.jpg";
            var numeroUltimoVolume = "Volume 01";
            var slugUltimoVolume = "volume-1";

            var obrasRepository = new ObrasFactory();
            var comic = obrasRepository
                .GerarComicComAdicaoVolume(titulo, tituloAlternativo, alias, autor, artista, ano, slug,
                    usuarioInclusao, usuarioAlteracao, imagemCapaPrincipal, sinopse,
                    imagemBanner, status, tipo, nacionalidade, observacao, imagemUltimoVolume, numeroUltimoVolume, slugUltimoVolume);

            Assert.NotNull(comic);
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", comic.Id.ToString());
            Assert.False(string.IsNullOrEmpty(comic.Titulo));
            Assert.Equal(titulo, comic.Titulo);
            Assert.False(string.IsNullOrEmpty(comic.TituloAlternativo));
            Assert.Equal(tituloAlternativo, comic.TituloAlternativo);
            Assert.False(string.IsNullOrEmpty(comic.Alias));
            Assert.Equal(alias, comic.Alias);
            Assert.False(string.IsNullOrEmpty(comic.Autor));
            Assert.Equal(autor, comic.Autor);
            Assert.False(string.IsNullOrEmpty(comic.Artista));
            Assert.Equal(artista, comic.Artista);
            Assert.False(string.IsNullOrEmpty(comic.Ano));
            Assert.Equal(ano, comic.Ano);
            Assert.False(string.IsNullOrEmpty(comic.Slug));
            Assert.Equal(slug, comic.Slug);
            Assert.Null(comic.Visualizacoes);
            Assert.False(string.IsNullOrEmpty(comic.UsuarioInclusao));
            Assert.Equal(usuarioInclusao, comic.UsuarioInclusao);
            Assert.False(string.IsNullOrEmpty(comic.UsuarioAlteracao));
            Assert.Equal(usuarioAlteracao, comic.UsuarioAlteracao);
            Assert.False(string.IsNullOrEmpty(comic.ImagemCapaPrincipal));
            Assert.Equal(imagemCapaPrincipal, comic.ImagemCapaPrincipal);
            Assert.False(string.IsNullOrEmpty(comic.Sinopse));
            Assert.Equal(sinopse, comic.Sinopse);
            Assert.NotEqual(new DateTime(), comic.DataInclusao);
            Assert.NotEqual(new DateTime(), comic.DataAlteracao);
            Assert.False(comic.EhObraMaiorIdade);
            Assert.False(comic.EhRecomendacao);
            Assert.False(string.IsNullOrEmpty(comic.CodigoCorHexaObra));
            Assert.False(string.IsNullOrEmpty(comic.ImagemBanner));
            Assert.Equal(imagemBanner, comic.ImagemBanner);
            Assert.False(string.IsNullOrEmpty(comic.CargoObraDiscord));
            Assert.False(string.IsNullOrEmpty(comic.DiretorioImagemObra));
            Assert.False(string.IsNullOrEmpty(comic.StatusObra));
            Assert.Equal(status, comic.StatusObra);
            Assert.False(string.IsNullOrEmpty(comic.TipoObra));
            Assert.Equal(tipo, comic.TipoObra);
            Assert.False(string.IsNullOrEmpty(comic.Nacionalidade));
            Assert.Equal(nacionalidade, comic.Nacionalidade);
            Assert.False(string.IsNullOrEmpty(comic.Observacao));
            Assert.Equal(observacao, comic.Observacao);
            Assert.False(comic.IntegracaoDiscord);
            Assert.False(string.IsNullOrEmpty(comic.ImagemCapaUltimoVolume));
            Assert.Equal(imagemUltimoVolume, comic.ImagemCapaUltimoVolume);
            Assert.False(string.IsNullOrEmpty(comic.NumeroUltimoVolume));
            Assert.Equal(numeroUltimoVolume, comic.NumeroUltimoVolume);
            Assert.False(string.IsNullOrEmpty(comic.SlugUltimoVolume));
            Assert.Equal(slugUltimoVolume, comic.SlugUltimoVolume);
            Assert.True(string.IsNullOrEmpty(comic.NumeroUltimoCapitulo));
            Assert.True(string.IsNullOrEmpty(comic.SlugUltimoCapitulo));
            Assert.Null(comic.DataAtualizacaoUltimoCapitulo);
            Assert.True(comic.Volumes.Count > 0);
        }

        [Fact]
        public void EntidadeComic_DeveCriarUmaComicComAdicaoDeCapitulo()
        {
            var titulo = "Hatsukoi Losstime";
            var tituloAlternativo = "初恋ロスタイム";
            var alias = "Hatsukoi Losstime";
            var autor = "Nishina Yuuki";
            var artista = "Nanora & Zerokich";
            var ano = "2019";
            var slug = "hatsukoi-losstime";
            var usuarioInclusao = "Bravo";
            var usuarioAlteracao = "Bravo";
            var imagemCapaPrincipal = "https://tsundoku.com.br/wp-content/uploads/2022/01/cover_hatsukoi_vol2.jpg";
            var sinopse = "Em um mundo onde apenas duas pessoas se moviam...";
            var imagemBanner = "https://tsundoku.com.br/wp-content/uploads/2022/01/HatsukoiEmbed.jpg";
            var status = "Em andamento";
            var tipo = "Mangá";
            var nacionalidade = "Japonesa";
            var observacao = "Uma obra muito boa";
            var imagemUltimoVolume = "https://tsundoku.com.br/wp-content/uploads/2022/01/Hatsukoi_cover.jpg";
            var numeroUltimoVolume = "Volume 01";
            var slugUltimoVolume = "volume-1";
            var numeroUltimoCapitulo = "Capítulo 01";
            var slugUltimoCapitulo = "capitulo-1";
            var dataAtualizacaoUltimoCapitulo = new DateTime(2025, 02, 13);

            var obrasRepository = new ObrasFactory();
            var comic = obrasRepository
                .GerarComicComAdicaoCapitulo(titulo, tituloAlternativo, alias, autor, artista, ano, slug, usuarioInclusao, usuarioAlteracao,
                    imagemCapaPrincipal, sinopse, imagemBanner, status, tipo, nacionalidade, observacao, imagemUltimoVolume,
                    numeroUltimoVolume, slugUltimoVolume, numeroUltimoCapitulo, slugUltimoCapitulo, dataAtualizacaoUltimoCapitulo);

            Assert.NotNull(comic);
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", comic.Id.ToString());
            Assert.False(string.IsNullOrEmpty(comic.Titulo));
            Assert.Equal(titulo, comic.Titulo);
            Assert.False(string.IsNullOrEmpty(comic.TituloAlternativo));
            Assert.Equal(tituloAlternativo, comic.TituloAlternativo);
            Assert.False(string.IsNullOrEmpty(comic.Alias));
            Assert.Equal(alias, comic.Alias);
            Assert.False(string.IsNullOrEmpty(comic.Autor));
            Assert.Equal(autor, comic.Autor);
            Assert.False(string.IsNullOrEmpty(comic.Artista));
            Assert.Equal(artista, comic.Artista);
            Assert.False(string.IsNullOrEmpty(comic.Ano));
            Assert.Equal(ano, comic.Ano);
            Assert.False(string.IsNullOrEmpty(comic.Slug));
            Assert.Equal(slug, comic.Slug);
            Assert.Null(comic.Visualizacoes);
            Assert.False(string.IsNullOrEmpty(comic.UsuarioInclusao));
            Assert.Equal(usuarioInclusao, comic.UsuarioInclusao);
            Assert.False(string.IsNullOrEmpty(comic.UsuarioAlteracao));
            Assert.Equal(usuarioAlteracao, comic.UsuarioAlteracao);
            Assert.False(string.IsNullOrEmpty(comic.ImagemCapaPrincipal));
            Assert.Equal(imagemCapaPrincipal, comic.ImagemCapaPrincipal);
            Assert.False(string.IsNullOrEmpty(comic.Sinopse));
            Assert.Equal(sinopse, comic.Sinopse);
            Assert.NotEqual(new DateTime(), comic.DataInclusao);
            Assert.NotEqual(new DateTime(), comic.DataAlteracao);
            Assert.False(comic.EhObraMaiorIdade);
            Assert.False(comic.EhRecomendacao);
            Assert.False(string.IsNullOrEmpty(comic.CodigoCorHexaObra));
            Assert.False(string.IsNullOrEmpty(comic.ImagemBanner));
            Assert.Equal(imagemBanner, comic.ImagemBanner);
            Assert.False(string.IsNullOrEmpty(comic.CargoObraDiscord));
            Assert.False(string.IsNullOrEmpty(comic.DiretorioImagemObra));
            Assert.False(string.IsNullOrEmpty(comic.StatusObra));
            Assert.Equal(status, comic.StatusObra);
            Assert.False(string.IsNullOrEmpty(comic.TipoObra));
            Assert.Equal(tipo, comic.TipoObra);
            Assert.False(string.IsNullOrEmpty(comic.Nacionalidade));
            Assert.Equal(nacionalidade, comic.Nacionalidade);
            Assert.False(string.IsNullOrEmpty(comic.Observacao));
            Assert.Equal(observacao, comic.Observacao);
            Assert.False(comic.IntegracaoDiscord);
            Assert.False(string.IsNullOrEmpty(comic.ImagemCapaUltimoVolume));
            Assert.Equal(imagemUltimoVolume, comic.ImagemCapaUltimoVolume);
            Assert.False(string.IsNullOrEmpty(comic.NumeroUltimoVolume));
            Assert.Equal(numeroUltimoVolume, comic.NumeroUltimoVolume);
            Assert.False(string.IsNullOrEmpty(comic.SlugUltimoVolume));
            Assert.Equal(slugUltimoVolume, comic.SlugUltimoVolume);
            Assert.False(string.IsNullOrEmpty(comic.NumeroUltimoCapitulo));
            Assert.Equal(numeroUltimoCapitulo, comic.NumeroUltimoCapitulo);
            Assert.False(string.IsNullOrEmpty(comic.SlugUltimoCapitulo));
            Assert.Equal(slugUltimoCapitulo, comic.SlugUltimoCapitulo);
            Assert.NotNull(comic.DataAtualizacaoUltimoCapitulo);
            Assert.Equal(dataAtualizacaoUltimoCapitulo, comic.DataAtualizacaoUltimoCapitulo);
            Assert.True(comic.Volumes.Count > 0);
            Assert.True(comic.Volumes[0].ListaCapitulo.Count > 0);
            Assert.True(comic.Volumes[0].ListaCapitulo[0].ListaImagensJson.Length > 0);
        }

        #endregion
    }
}