using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers;

namespace TsundokuTraducoes.Entities.Tests.Volumes
{
    public class VolumeNovelTestes
    {
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
    }
}