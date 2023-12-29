using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers;

namespace TsundokuTraducoes.Entities.Tests.Obras
{
    public class ComicTestes
    {
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
                                  "em-andamento",
                                  "comic",
                                  "japonesa");

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
                                  "em-andamento",
                                  "comic",
                                  "japonesa");

            comic.AtualizaDadosUltimoVolume("https://tsundoku.com.br/wp-content/uploads/2022/01/Hatsukoi_cover.jpg", "Volume 01", "volume-1");
            comic.AtualizaDadosUltimoCapitulo("Capítulo 01", "capitulo-1", DateTime.Now);

            Assert.Empty(comic.Titulo);
        }
    }
}
