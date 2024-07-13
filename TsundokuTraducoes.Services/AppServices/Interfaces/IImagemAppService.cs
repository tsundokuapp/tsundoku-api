using FluentResults;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Services.AppServices.Interfaces
{
    public interface IImagemAppService
    {
        Task<Result> ProcessaUploadCapaObra(ObraDTO obraDTO, bool alterarImagem);
        Task<Result> ProcessaUploadBannerObra(ObraDTO obraDTO, bool alterarImagem);
        Task<Result> ProcessaUploadCapaVolume(VolumeDTO volumeDTO, string numeroVolume, string diretorioImagemObra, bool alterarImagem);
        Task<Result> ProcessaUploadListaImagensCapituloNovel(CapituloDTO capituloDTO, VolumeNovel volume, bool alterarImagem, int? ordemPaginaImagem = null);
        Task<Result> ProcessaUploadListaImagensCapituloManga(CapituloDTO capituloDTO, VolumeComic volume, bool alterarImagem, int? ordemPaginaImagem = null);
        Task<bool> ExcluiDiretorioImagens(string diretorioImagens, bool diretorioLocal);
    }
}