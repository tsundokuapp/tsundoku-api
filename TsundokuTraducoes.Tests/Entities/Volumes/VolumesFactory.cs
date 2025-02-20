using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers;

namespace TsundokuTraducoes.Tests.Entities.Volumes
{
    public class VolumesFactory
    {
        #region => MOCK VOLUME NOVEL

        public VolumeNovel GerarVolumeNovel(Guid? idObra, Guid? idVolume)
        {
            var volume = new VolumeNovel();
            volume.AdicionaVolume(
                idVolume ?? Guid.NewGuid(),
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
                idObra ?? Guid.NewGuid());

            return volume;
        }

        public VolumeNovel GerarVolumeNovelComTituloSinopse(Guid? idObra, Guid? idVolume)
        {
            var volume = new VolumeNovel();
            volume.AdicionaVolume(
                idVolume ?? Guid.NewGuid(),
                "1",
                "https://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg",
                "volume-1",
                "Início da Jornada",
                "Uma história de uma Bruxa comilona",
                "Bravo",
                "Bravo",
                DateTime.Now,
                DateTime.Now,
                Diretorios.RetornaDiretorioImagemCriado("BruxaErrante", "Volume01"),
                idObra ?? Guid.NewGuid());

            return volume;
        }

        public VolumeNovel GerarVolumeNovelComCapitulo(Guid? idObra, Guid? idVolume)
        {
            var volume = new VolumeNovel();
            volume.AdicionaVolume(
                idVolume ?? Guid.NewGuid(),
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
                idObra ?? Guid.NewGuid());

            var capituloNovel = new CapituloNovel() { VolumeId = volume.Id, ConteudoNovel = "<html>Conteúdo Novel aqui</html>" };
            volume.ListaCapitulo.Add(capituloNovel);

            return volume;
        }

        #endregion

        #region => MOCK VOLUME COMIC

        public VolumeComic GerarVolumeComic(Guid? idObra, Guid? idVolume)
        {
            var volume = new VolumeComic();
            volume.AdicionaVolume(
                idVolume ?? Guid.NewGuid(),
                "1",
                "https://tsundoku.com.br/wp-content/uploads/2021/12/Shadow-Manga-V13.jpg",
                "volume-1",
                "",
                "",
                "Bravo",
                "Bravo",
                DateTime.Now,
                DateTime.Now,
                Diretorios.RetornaDiretorioImagemCriado("Shadow", "Volume01"),
                idObra ?? Guid.NewGuid());

            return volume;
        }

        public VolumeComic GerarVolumeComicComTituloSinopse(Guid? idObra, Guid? idVolume)
        {
            var volume = new VolumeComic();
            volume.AdicionaVolume(
                idVolume ?? Guid.NewGuid(),
                "1",
                "https://tsundoku.com.br/wp-content/uploads/2021/12/Shadow-Manga-V13.jpg",
                "volume-1",
                "Shadow Garden",
                "Quando um chuunibyou vai para outro mundo, o que será acontece?",
                "Bravo",
                "Bravo",
                DateTime.Now,
                DateTime.Now,
                Diretorios.RetornaDiretorioImagemCriado("Shadow", "Volume01"),
                idObra ?? Guid.NewGuid());

            return volume;
        }

        public VolumeComic GerarVolumeComicComCapitulo(Guid? idObra, Guid? idVolume)
        {
            var volume = new VolumeComic();
            volume.AdicionaVolume(
                idVolume ?? Guid.NewGuid(),
                "1",
                "https://tsundoku.com.br/wp-content/uploads/2021/12/Shadow-Manga-V13.jpg",
                "volume-1",
                "",
                "",
                "Bravo",
                "Bravo",
                DateTime.Now,
                DateTime.Now,
                Diretorios.RetornaDiretorioImagemCriado("Shadow", "Volume01"),
                idObra ?? Guid.NewGuid());

            var capituloComic = new CapituloComic() { VolumeId = volume.Id, ListaImagensJson = "[]" };
            volume.ListaCapitulo.Add(capituloComic);

            return volume;
        }

        #endregion
    }
}
