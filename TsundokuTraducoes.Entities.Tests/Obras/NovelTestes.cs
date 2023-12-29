using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers;

namespace TsundokuTraducoes.Entities.Tests.Obras
{
    public class NovelTestes
    {
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
                                  "em-andamento",
                                  "light-novel",
                                  "japonesa");

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
                                  "em-andamento",
                                  "light-novel",
                                  "japonesa");

            novel.AtualizaDadosUltimoVolume("https://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg", "Volume 01", "volume-1");
            novel.AtualizaDadosUltimoCapitulo("Ilustrações", "ilustracoes", DateTime.Now);

            Assert.Empty(novel.ImagemCapaPrincipal);
        }
    }
}
