using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers;

namespace TsundokuTraducoes.Tests.Infrastructure.Data.Repositories.Obras
{
    public class ObraRepositoryFactory
    {
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
                false,
                true);

            return novel;
        }

        public Comic GerarComic()
        {
            var comic = new Comic();
            comic.AdicionaComic(
                Guid.NewGuid(),
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
                "Em Andamento",
                "Mangá",
                "japonesa",
                "",
                false);

            return comic;
        }
    }
}
