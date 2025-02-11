using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers;

namespace TsundokuTraducoes.Tests.Entities.Obras
{
    public class ObrasFactory
    {
        #region => MOCK NOVEL

        public Novel GerarNovel()
        {
            var novel = new Novel();
            novel.AdicionaNovel(
                Guid.NewGuid(),
                "Bruxa Errante, a Jornada dos Testes",
                "Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々",
                "Bruxa Errante",
                "Shiraishi Jougi",
                "Azure",
                "2017",
                "bruxa-errante-a-jornada-dos-testes",
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
                "Em Andamento",
                "Light Novel",
                "japonesa",
                "Uma obra muito boa",
                false);

            novel.GenerosNovel = new List<GeneroNovel> { 
                new GeneroNovel { GeneroId = Guid.NewGuid(), NovelId = Guid.NewGuid() } 
            };

            return novel;
        }

        public Novel GerarNovelComAdicaoVolume()
        {
            var novel = new Novel();
            novel.AdicionaNovel(
                Guid.NewGuid(),
                "Bruxa Errante, a Jornada dos Testes",
                "Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々",
                "Bruxa Errante",
                "Shiraishi Jougi",
                "Azure",
                "2017",
                "bruxa-errante-a-jornada-dos-testes",
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
                "Em Andamento",
                "Light Novel",
                "japonesa",
                "Uma obra muito boa",
                false);

            novel.GenerosNovel = new List<GeneroNovel> 
            {
                new GeneroNovel { GeneroId = Guid.NewGuid(), NovelId = Guid.NewGuid() }
            };

            novel.Volumes = new List<VolumeNovel>
            {
                new VolumeNovel()
            };

            novel.AtualizaDadosUltimoVolume("https://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg", "Volume 01", "volume-1");

            return novel;
        }

        public Novel GerarNovelComAdicaoCapitulo()
        {
            var novel = new Novel();
            novel.AdicionaNovel(
                Guid.NewGuid(),
                "Bruxa Errante, a Jornada dos Testes",
                "Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々",
                "Bruxa Errante",
                "Shiraishi Jougi",
                "Azure",
                "2017",
                "bruxa-errante-a-jornada-dos-testes",
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
                "Em Andamento",
                "Light Novel",
                "japonesa",
                "Uma obra muito boa",
                false);

            novel.GenerosNovel = new List<GeneroNovel> 
            {
                new GeneroNovel { GeneroId = Guid.NewGuid(), NovelId = Guid.NewGuid() }
            };

            var volumeNovel = new VolumeNovel();
            volumeNovel.ListaCapitulo = new List<CapituloNovel>
            {
                new CapituloNovel()
            };

            novel.Volumes = new List<VolumeNovel> { volumeNovel };

            novel.AtualizaDadosUltimoVolume("https://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg", "Volume 01", "volume-1");
            novel.AtualizaDadosUltimoCapitulo("Ilustrações", "ilustracoes", DateTime.Now);

            return novel;
        }

        #endregion

        #region => MOCK COMIC

        public Comic GerarComic()
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

            return comic;
        }

        public Comic GerarComicComAdicaoVolume()
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

            comic.GenerosComic = new List<GeneroComic>
            {
                new GeneroComic { GeneroId = Guid.NewGuid(), ComicId = Guid.NewGuid() }
            };

            comic.Volumes = new List<VolumeComic>
            {
                new VolumeComic()
            };

            comic.AtualizaDadosUltimoVolume("https://tsundoku.com.br/wp-content/uploads/2022/01/Hatsukoi_cover.jpg", "Volume 01", "volume-1");
            return comic;
        }

        public Comic GerarComicComAdicaoCapitulo()
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

            comic.GenerosComic = new List<GeneroComic>
            {
                new GeneroComic { GeneroId = Guid.NewGuid(), ComicId = Guid.NewGuid() }
            };

            var volumeComic = new VolumeComic();            
            var capituloComic = new CapituloComic();
            capituloComic.ListaImagensJson = "[]";            
            volumeComic.ListaCapitulo = new List<CapituloComic>{ capituloComic };
            comic.Volumes = new List<VolumeComic> { volumeComic };

            comic.AtualizaDadosUltimoVolume("https://tsundoku.com.br/wp-content/uploads/2022/01/Hatsukoi_cover.jpg", "Volume 01", "volume-1");
            comic.AtualizaDadosUltimoCapitulo("Capítulo 01", "capitulo-1", DateTime.Now);
            return comic;
        }

        #endregion
    }
}
