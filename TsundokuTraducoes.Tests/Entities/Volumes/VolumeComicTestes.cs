using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers;

namespace TsundokuTraducoes.Entities.Tests.Volumes
{
    public class VolumeComicTestes
    {
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
    }
}
