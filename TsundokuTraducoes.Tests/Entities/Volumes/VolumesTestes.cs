using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers;

namespace TsundokuTraducoes.Tests.Entities.Volumes
{
    public class VolumesTestes
    {
        #region TESTES - VOLUMES NOVELS

        [Fact]
        public void CriaVolumeNovelValido()
        {
            var volumeNovel = new VolumeNovel();
            volumeNovel.AdicionaVolume(
                Guid.Parse("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                "1",
                "https://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg",
                "volume-1",
                "",
                "",
                "Bravo",
                "Bravo",
                DateTime.Now,
                DateTime.Now,
                Diretorios.RetornaDiretorioImagemCriado("BruxaErrante", "Volume01"),
                Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"));

            Assert.Equal("volume-1", volumeNovel.Slug);
            Assert.NotEmpty(volumeNovel.ImagemVolume);
            Assert.NotNull(volumeNovel);
        }

        [Fact]
        public void DeveFalharAoCriarSemImagemDeCapa()
        {
            var volumeNovel = new VolumeNovel();
            volumeNovel.AdicionaVolume(
                Guid.Parse("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                "1",
                "",
                "volume-2",
                "",
                "",
                "Bravo",
                "Bravo",
                DateTime.Now,
                DateTime.Now,
                Diretorios.RetornaDiretorioImagemCriado("BruxaErrante", "Volume01"),
                Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"));

            Assert.Empty(volumeNovel.ImagemVolume);
        }

        [Fact]
        public void EntidadeVolumeNovel_DeveCriarUmVolumeNovel_SemCapitulo()
        {
            var volumesFactory = new VolumesFactory();
            var idObra = Guid.NewGuid();
            var idVolume = Guid.NewGuid();
            var volumeNovel = volumesFactory.GerarVolumeNovel(idObra, idVolume);
            var numeroDeCamposDoVolumeNovel = volumeNovel.GetType().GetProperties().Length;

            Assert.NotNull(volumeNovel);
            Assert.False(string.IsNullOrEmpty(volumeNovel.Numero));
            Assert.False(string.IsNullOrEmpty(volumeNovel.ImagemVolume));
            Assert.False(string.IsNullOrEmpty(volumeNovel.Slug));
            Assert.True(string.IsNullOrEmpty(volumeNovel.Titulo));
            Assert.True(string.IsNullOrEmpty(volumeNovel.Sinopse));
            Assert.False(string.IsNullOrEmpty(volumeNovel.UsuarioInclusao));
            Assert.False(string.IsNullOrEmpty(volumeNovel.UsuarioAlteracao));
            Assert.NotEqual(new DateTime(), volumeNovel.DataInclusao);
            Assert.NotEqual(new DateTime(), volumeNovel.DataAlteracao);
            Assert.False(string.IsNullOrEmpty(volumeNovel.DiretorioImagemVolume));
            Assert.False(volumeNovel.ListaCapitulo.Any());
            Assert.Equal(14, numeroDeCamposDoVolumeNovel);
        }

        [Fact]
        public void EntidadeVolumeNovel_DeveCriarUmVolumeNovelComTituloSinopse_SemCapitulo()
        {
            var volumesFactory = new VolumesFactory();
            var idObra = Guid.NewGuid();
            var idVolume = Guid.NewGuid();
            var volumeNovel = volumesFactory.GerarVolumeNovelComTituloSinopse(idObra, idVolume);
            var numeroDeCamposDoVolumeNovel = volumeNovel.GetType().GetProperties().Length;

            Assert.NotNull(volumeNovel);
            Assert.False(string.IsNullOrEmpty(volumeNovel.Numero));
            Assert.False(string.IsNullOrEmpty(volumeNovel.ImagemVolume));
            Assert.False(string.IsNullOrEmpty(volumeNovel.Slug));
            Assert.False(string.IsNullOrEmpty(volumeNovel.Titulo));
            Assert.False(string.IsNullOrEmpty(volumeNovel.Sinopse));
            Assert.False(string.IsNullOrEmpty(volumeNovel.UsuarioInclusao));
            Assert.False(string.IsNullOrEmpty(volumeNovel.UsuarioAlteracao));
            Assert.NotEqual(new DateTime(), volumeNovel.DataInclusao);
            Assert.NotEqual(new DateTime(), volumeNovel.DataAlteracao);
            Assert.False(string.IsNullOrEmpty(volumeNovel.DiretorioImagemVolume));
            Assert.False(volumeNovel.ListaCapitulo.Any());
            Assert.Equal(14, numeroDeCamposDoVolumeNovel);

        }

        [Fact]
        public void EntidadeVolumeNovel_DeveCriarUmVolumeNovel_ComCapitulo()
        {
            var volumesFactory = new VolumesFactory();
            var idObra = Guid.NewGuid();
            var idVolume = Guid.NewGuid();
            var volumeNovel = volumesFactory.GerarVolumeNovelComCapitulo(idObra, idVolume);
            var numeroDeCamposDoVolumeNovel = volumeNovel.GetType().GetProperties().Length;

            Assert.NotNull(volumeNovel);
            Assert.False(string.IsNullOrEmpty(volumeNovel.Numero));
            Assert.False(string.IsNullOrEmpty(volumeNovel.ImagemVolume));
            Assert.False(string.IsNullOrEmpty(volumeNovel.Slug));
            Assert.True(string.IsNullOrEmpty(volumeNovel.Titulo));
            Assert.True(string.IsNullOrEmpty(volumeNovel.Sinopse));
            Assert.False(string.IsNullOrEmpty(volumeNovel.UsuarioInclusao));
            Assert.False(string.IsNullOrEmpty(volumeNovel.UsuarioAlteracao));
            Assert.NotEqual(new DateTime(), volumeNovel.DataInclusao);
            Assert.NotEqual(new DateTime(), volumeNovel.DataAlteracao);
            Assert.False(string.IsNullOrEmpty(volumeNovel.DiretorioImagemVolume));
            Assert.True(volumeNovel.ListaCapitulo.Any());
            Assert.Equal(14, numeroDeCamposDoVolumeNovel);
        }

        #endregion

        #region TESTES - VOLUMES COMICS

        [Fact]
        public void CriaVolumeComicValido()
        {
            var volumeComic = new VolumeComic();
            volumeComic.AdicionaVolume(
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                "1",
                "https://tsundoku.com.br/wp-content/uploads/2022/01/Hatsukoi_cover.jpg",
                "volume-1",
                "",
                "",
                "Bravo",
                "Bravo",
                DateTime.Now,
                DateTime.Now,
                Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime", "Volume01"),
                Guid.Parse("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"));

            Assert.Equal("volume-1", volumeComic.Slug);
            Assert.NotEmpty(volumeComic.ImagemVolume);
            Assert.NotNull(volumeComic);
        }

        [Fact]
        public void DeveFalharSeCriarSemAlgumNumero()
        {
            var volumeComic = new VolumeComic();
            volumeComic.AdicionaVolume(
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                "",
                "",
                "volume-1",
                "",
                "",
                "Bravo",
                "Bravo",
                DateTime.Now,
                DateTime.Now,
                Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime", "Volume01"),
                Guid.Parse("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"));

            Assert.Empty(volumeComic.Numero);
        }

        [Fact]
        public void EntidadeVolumeComic_DeveCriarUmVolumeComic_SemCapitulo()
        {
            var volumesFactory = new VolumesFactory();
            var idObra = Guid.NewGuid();
            var idVolume = Guid.NewGuid();
            var volumeComic = volumesFactory.GerarVolumeComic(idObra, idVolume);
            var numeroDeCamposDoVolumeComic = volumeComic.GetType().GetProperties().Length;

            Assert.NotNull(volumeComic);
            Assert.False(string.IsNullOrEmpty(volumeComic.Numero));
            Assert.False(string.IsNullOrEmpty(volumeComic.ImagemVolume));
            Assert.False(string.IsNullOrEmpty(volumeComic.Slug));
            Assert.True(string.IsNullOrEmpty(volumeComic.Titulo));
            Assert.True(string.IsNullOrEmpty(volumeComic.Sinopse));
            Assert.False(string.IsNullOrEmpty(volumeComic.UsuarioInclusao));
            Assert.False(string.IsNullOrEmpty(volumeComic.UsuarioAlteracao));
            Assert.NotEqual(new DateTime(), volumeComic.DataInclusao);
            Assert.NotEqual(new DateTime(), volumeComic.DataAlteracao);
            Assert.False(string.IsNullOrEmpty(volumeComic.DiretorioImagemVolume));
            Assert.False(volumeComic.ListaCapitulo.Any());
            Assert.Equal(14, numeroDeCamposDoVolumeComic);
        }

        [Fact]
        public void EntidadeVolumeComic_DeveCriarUmVolumeComicComTituloSinopse_SemCapitulo()
        {
            var volumesFactory = new VolumesFactory();
            var idObra = Guid.NewGuid();
            var idVolume = Guid.NewGuid();
            var volumeComic = volumesFactory.GerarVolumeComicComTituloSinopse(idObra, idVolume);
            var numeroDeCamposDoVolumeComic = volumeComic.GetType().GetProperties().Length;

            Assert.NotNull(volumeComic);
            Assert.False(string.IsNullOrEmpty(volumeComic.Numero));
            Assert.False(string.IsNullOrEmpty(volumeComic.ImagemVolume));
            Assert.False(string.IsNullOrEmpty(volumeComic.Slug));
            Assert.False(string.IsNullOrEmpty(volumeComic.Titulo));
            Assert.False(string.IsNullOrEmpty(volumeComic.Sinopse));
            Assert.False(string.IsNullOrEmpty(volumeComic.UsuarioInclusao));
            Assert.False(string.IsNullOrEmpty(volumeComic.UsuarioAlteracao));
            Assert.NotEqual(new DateTime(), volumeComic.DataInclusao);
            Assert.NotEqual(new DateTime(), volumeComic.DataAlteracao);
            Assert.False(string.IsNullOrEmpty(volumeComic.DiretorioImagemVolume));
            Assert.False(volumeComic.ListaCapitulo.Any());
            Assert.Equal(14, numeroDeCamposDoVolumeComic);

        }

        [Fact]
        public void EntidadeVolumeComic_DeveCriarUmVolumeComic_ComCapitulo()
        {
            var volumesFactory = new VolumesFactory();
            var idObra = Guid.NewGuid();
            var idVolume = Guid.NewGuid();
            var volumeComic = volumesFactory.GerarVolumeComicComCapitulo(idObra, idVolume);
            var numeroDeCamposDoVolumeComic = volumeComic.GetType().GetProperties().Length;

            Assert.NotNull(volumeComic);
            Assert.False(string.IsNullOrEmpty(volumeComic.Numero));
            Assert.False(string.IsNullOrEmpty(volumeComic.ImagemVolume));
            Assert.False(string.IsNullOrEmpty(volumeComic.Slug));
            Assert.True(string.IsNullOrEmpty(volumeComic.Titulo));
            Assert.True(string.IsNullOrEmpty(volumeComic.Sinopse));
            Assert.False(string.IsNullOrEmpty(volumeComic.UsuarioInclusao));
            Assert.False(string.IsNullOrEmpty(volumeComic.UsuarioAlteracao));
            Assert.NotEqual(new DateTime(), volumeComic.DataInclusao);
            Assert.NotEqual(new DateTime(), volumeComic.DataAlteracao);
            Assert.False(string.IsNullOrEmpty(volumeComic.DiretorioImagemVolume));
            Assert.True(volumeComic.ListaCapitulo.Any());
            Assert.Equal(14, numeroDeCamposDoVolumeComic);
        }

        #endregion
    }
}
