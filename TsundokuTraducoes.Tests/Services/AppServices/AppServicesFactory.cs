using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers;

namespace TsundokuTraducoes.Services.AppServices;

public class AppServicesFactory
{
    public CapituloNovel GerarCapituloNovel(Guid? idCapitulo, Guid? IdVolume)
    {
        return new CapituloNovel()
        {
            Id = idCapitulo ?? Guid.NewGuid(),
            VolumeId = IdVolume ?? Guid.NewGuid(),
            Numero = "1",
            Parte = "1",
            OrdemCapitulo = 1,
            Titulo = "Capítulo 1",
            ConteudoNovel = "Conteúdo do capítulo 1",
            Slug = "capitulo-1",
            UsuarioInclusao = "UsuarioTeste",
            UsuarioAlteracao = "UsuarioTeste",
            DataInclusao = new DateTime(2024, 1, 1, 10, 30, 0),
            DataAlteracao = new DateTime(2024, 1, 2, 14, 45, 0),
            EhIlustracoesNovel = false,
            Tradutor = "TradutorTeste",
            Revisor = "RevisorTeste",
            QC = "QCTeste",
            ListaImagensJson = "[]",
            Publicado = true
        };
    }

    public VolumeNovel GerarVolumeNovel(Guid? idVolume)
    {
        var volume = new VolumeNovel();
        volume.AdicionaVolume(
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
        
        return volume;
    }
}