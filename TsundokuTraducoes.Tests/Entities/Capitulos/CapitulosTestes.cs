using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Helpers;

namespace TsundokuTraducoes.Tests.Entities.Capitulos
{
    public class CapitulosTestes
    {
        #region => TESTES - CAPÍTULOS NOVELS

        [Fact]
        public void CriarCapituloNovelIlustracoesValido()
        {
            var caputitulosFactory = new CapitulosFactory();
            var capituloNovelIlustracoes = new CapituloNovel();
            capituloNovelIlustracoes.AdicionaCapitulo(
                    Guid.Parse("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                    "Ilustrações",
                    "",
                    0,
                    "",
                    "",
                    "ilustracoes",
                    "Bravo",
                    "Bravo",
                    DateTime.Now,
                    DateTime.Now,
                    Diretorios.RetornaDiretorioImagemCriado("BruxaErrante", "Volume01", "Ilustracoes"),
                    true,
                    "",
                    "",
                    "",
                    Guid.Parse("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                    caputitulosFactory.RetornaConteudoNovelIlustracoes(),
                    false
                    );

            Assert.Equal("Ilustrações", capituloNovelIlustracoes.Numero);
            Assert.NotEmpty(capituloNovelIlustracoes.ListaImagensJson);
            Assert.NotNull(capituloNovelIlustracoes);
        }

        [Fact]
        public void DeveFalharAoCriarSemConteudoDasIlustracoes()
        {
            var capituloNovelIlustracoes = new CapituloNovel();
            capituloNovelIlustracoes.AdicionaCapitulo(
                    Guid.Parse("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                    "Ilustra",
                    "",
                    0,
                    "",
                    "",
                    "ilustracoes",
                    "Bravo",
                    "Bravo",
                    DateTime.Now,
                    DateTime.Now,
                    Diretorios.RetornaDiretorioImagemCriado("BruxaErrante", "Volume01", "Ilustracoes"),
                    true,
                    "",
                    "",
                    "",
                    Guid.Parse("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                    "",
                    false);

            Assert.Empty(capituloNovelIlustracoes.ConteudoNovel);
        }

        [Fact]
        public void CriarCapituloNovelValido()
        {
            var caputitulosFactory = new CapitulosFactory();
            var capituloNovelIlustracoes = new CapituloNovel();
            capituloNovelIlustracoes.AdicionaCapitulo(
                    Guid.Parse("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                    "1",
                    "",
                    1,
                    "País dos Magos",
                    caputitulosFactory.RetornaConteudoNovel(),
                    "capitulo-1-pais-dos-magos",
                    "Bravo",
                    "Bravo",
                    DateTime.Now,
                    DateTime.Now,
                    Diretorios.RetornaDiretorioImagemCriado("BruxaErrante", "Volume01", "Ilustracoes"),
                    false,
                    "",
                    "",
                    "",
                    Guid.Parse("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                    "",
                    false);

            Assert.Equal("capitulo-1-pais-dos-magos", capituloNovelIlustracoes.Slug);
            Assert.NotEmpty(capituloNovelIlustracoes.ConteudoNovel);
            Assert.NotNull(capituloNovelIlustracoes);
        }

        [Fact]
        public void DeveFalharAoCriarSemConteudoDoCapitulo()
        {
            var capituloNovelIlustracoes = new CapituloNovel();
            capituloNovelIlustracoes.AdicionaCapitulo(
                    Guid.Parse("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                    "1",
                    "",
                    1,
                    "País",
                    "",
                    "capitulo-1-pais-dos-magos",
                    "Bravo",
                    "Bravo",
                    DateTime.Now,
                    DateTime.Now,
                    Diretorios.RetornaDiretorioImagemCriado("BruxaErrante", "Volume01", "Ilustracoes"),
                    false,
                    "",
                    "",
                    "",
                    Guid.Parse("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                    "",
                    false);

            Assert.Empty(capituloNovelIlustracoes.ConteudoNovel);
        }

        [Fact]
        public void EntidadeCapituloNovel_DeveCriarUmCapituloNovel()
        {
            var capitulosFactory = new CapitulosFactory();
            var idVolume = Guid.NewGuid();
            var capituloNovel = capitulosFactory.GerarCapituloNovel(idVolume);
            var quantidadeCamposCapituloNovel = capituloNovel.GetType().GetProperties().Length;

            Assert.NotNull(capituloNovel);
            Assert.False(string.IsNullOrEmpty(capituloNovel.Numero));
            Assert.True(string.IsNullOrEmpty(capituloNovel.Parte));
            Assert.Equal(1, capituloNovel.OrdemCapitulo);
            Assert.False(string.IsNullOrEmpty(capituloNovel.Titulo));
            Assert.False(string.IsNullOrEmpty(capituloNovel.ConteudoNovel));
            Assert.False(string.IsNullOrEmpty(capituloNovel.Slug));
            Assert.False(string.IsNullOrEmpty(capituloNovel.UsuarioInclusao));
            Assert.False(string.IsNullOrEmpty(capituloNovel.UsuarioAlteracao));
            Assert.NotEqual(new DateTime(), capituloNovel.DataInclusao);
            Assert.NotEqual(new DateTime(), capituloNovel.DataAlteracao);
            Assert.False(string.IsNullOrEmpty(capituloNovel.UsuarioAlteracao));
            Assert.False(capituloNovel.EhIlustracoesNovel);
            Assert.False(string.IsNullOrEmpty(capituloNovel.Tradutor));
            Assert.False(string.IsNullOrEmpty(capituloNovel.Revisor));
            Assert.False(string.IsNullOrEmpty(capituloNovel.QC));
            Assert.True(string.IsNullOrEmpty(capituloNovel.ListaImagensJson));
            Assert.True(capituloNovel.Publicado);
            Assert.Equal(20, quantidadeCamposCapituloNovel);
        }

        [Fact]
        public void EntidadeCapituloNovel_DeveCriarUmCapituloNovel_Ilustracoes()
        {
            var capitulosFactory = new CapitulosFactory();
            var idVolume = Guid.NewGuid();
            var capituloNovel = capitulosFactory.GerarCapituloNovelIlustracoes(idVolume);
            var quantidadeCamposCapituloNovel = capituloNovel.GetType().GetProperties().Length;

            Assert.NotNull(capituloNovel);
            Assert.False(string.IsNullOrEmpty(capituloNovel.Numero));
            Assert.True(string.IsNullOrEmpty(capituloNovel.Parte));
            Assert.Equal(1, capituloNovel.OrdemCapitulo);
            Assert.True(string.IsNullOrEmpty(capituloNovel.Titulo));
            Assert.True(string.IsNullOrEmpty(capituloNovel.ConteudoNovel));
            Assert.False(string.IsNullOrEmpty(capituloNovel.Slug));
            Assert.False(string.IsNullOrEmpty(capituloNovel.UsuarioInclusao));
            Assert.False(string.IsNullOrEmpty(capituloNovel.UsuarioAlteracao));
            Assert.NotEqual(new DateTime(), capituloNovel.DataInclusao);
            Assert.NotEqual(new DateTime(), capituloNovel.DataAlteracao);
            Assert.False(string.IsNullOrEmpty(capituloNovel.UsuarioAlteracao));
            Assert.True(capituloNovel.EhIlustracoesNovel);
            Assert.False(string.IsNullOrEmpty(capituloNovel.Tradutor));
            Assert.False(string.IsNullOrEmpty(capituloNovel.Revisor));
            Assert.False(string.IsNullOrEmpty(capituloNovel.QC));
            Assert.False(string.IsNullOrEmpty(capituloNovel.ListaImagensJson));
            Assert.True(capituloNovel.Publicado);
            Assert.Equal(20, quantidadeCamposCapituloNovel);
        }

        #endregion

        #region => TESTES - CAPÍTULOS COMICS

        [Fact]
        public void CapituloComicValido()
        {
            var caputitulosFactory = new CapitulosFactory();
            var capituloComic = new CapituloComic();
            capituloComic.AdicionaCapitulo(
            Guid.Parse("08dba6c0-f903-469b-866c-223f5ab45e56"),
            "1",
            1,
            "",
            "",
            caputitulosFactory.RetornaConteudoManga(),
            "capitulo-1",
            "Bravo",
            "Bravo",
            DateTime.Now,
            DateTime.Now,
            Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime", "Volume01", "Capitulo01"),
            Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
            false);

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
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                false);

            Assert.Empty(capituloComic.ListaImagensJson);
        }

        [Fact]
        public void EntidadeCapituloComic_DeveCriarUmCapituloComic()
        {
            var capitulosFactory = new CapitulosFactory();
            var idVolume = Guid.NewGuid();
            var capituloComic = capitulosFactory.GerarCapituloComic(idVolume);
            var quantidadeCamposCapituloComic = capituloComic.GetType().GetProperties().Length;

            Assert.NotNull(capituloComic);
            Assert.False(string.IsNullOrEmpty(capituloComic.Numero));
            Assert.Equal(1, capituloComic.OrdemCapitulo);
            Assert.True(string.IsNullOrEmpty(capituloComic.Parte));
            Assert.True(string.IsNullOrEmpty(capituloComic.Titulo));
            Assert.False(string.IsNullOrEmpty(capituloComic.ListaImagensJson));
            Assert.False(string.IsNullOrEmpty(capituloComic.Slug));
            Assert.False(string.IsNullOrEmpty(capituloComic.UsuarioInclusao));
            Assert.False(string.IsNullOrEmpty(capituloComic.UsuarioAlteracao));
            Assert.NotEqual(new DateTime(), capituloComic.DataInclusao);
            Assert.NotEqual(new DateTime(), capituloComic.DataAlteracao);
            Assert.False(string.IsNullOrEmpty(capituloComic.DiretorioImagemCapitulo));
            Assert.True(capituloComic.Publicado);
            Assert.Equal(15, quantidadeCamposCapituloComic);
        }

        #endregion
    }
}
