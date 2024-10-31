using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Helpers;

namespace TsundokuTraducoes.Entities.Tests.Capitulos
{
    public class CapituloComicTestes
    {
        [Fact]
        public void CapituloComicValido()
        {
            var capituloComic = new CapituloComic();
            capituloComic.AdicionaCapitulo(
            Guid.Parse("08dba6c0-f903-469b-866c-223f5ab45e56"),
            "1",
            1,
            "",
            "",
            RetornaConteudoManga(),
            "capitulo-1",
            "Bravo",
            "Bravo",
            DateTime.Now,
            DateTime.Now,
            Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime", "Volume01", "Capitulo01"),
            Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"));

            Assert.Equal("capitulo-1", capituloComic.Slug);
            Assert.NotEmpty(capituloComic.ListaImagensJson);
            Assert.NotEmpty(capituloComic.Id.ToString());
            Assert.NotNull(capituloComic);
        }

        [Fact]
        public void DeveFalharAoCriarSemImagens()
        {
            var capituloComic = new CapituloComic();
            capituloComic.AdicionaCapitulo(
                Guid.Parse("08dba6c0-f903-469b-866c-223f5ab45e56"),
                "1",
                1,
                "",
                "",
                "",
                "capitulo-1",
                "Bravo",
                "Bravo",
                DateTime.Now,
                DateTime.Now,
                Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime", "Volume01", "Capitulo01"),
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"));

            Assert.Empty(capituloComic.ListaImagensJson);
        }

        private string RetornaConteudoManga()
        {
            return @"[{\""Id\"": 1,\""Ordem\"": 1,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/0-46.jpg\""},{\""Id\"": 2,\""Ordem\"": 2,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/0-47.jpg\""},{\""Id\"": 3,\""Ordem\"": 3,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/1-60.jpg\""},{\""Id\"": 4,\""Ordem\"": 4,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/2-60.jpg\""},{\""Id\"": 5,\""Ordem\"": 5,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/3-61.jpg\""},{\""Id\"": 6,\""Ordem\"": 6,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/4-61.jpg\""},{\""Id\"": 7,\""Ordem\"": 7,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/5-61.jpg\""},{\""Id\"": 8,\""Ordem\"": 8,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/6-61.jpg\""},{\""Id\"": 9,\""Ordem\"": 9,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/7-61.jpg\""},{\""Id\"": 10,\""Ordem\"": 10,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/8-61.jpg\""},{\""Id\"": 10,\""Ordem\"": 10,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/9-61.jpg\""},{\""Id\"": 10,\""Ordem\"": 10,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/10-108.jpg\""}]";
        }
    }
}
