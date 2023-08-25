using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models.Obra;
using TsundokuTraducoes.Api.Models.Volume;

namespace TsundokuTraducoes.Api.Repository.Interfaces
{
    public interface IVolumeRepository
    {
        void AdicionaVolume(VolumeNovel volume);
        void ExcluiVolume(VolumeNovel volume);
        bool AlteracoesSalvas();
        VolumeNovel AtualizaVolume(VolumeDTO VolumeDTO);
        Task<List<VolumeNovel>> RetornaListaVolumes(int? idObra);
        Task<VolumeNovel> RetornaVolumePorId(int volumeId);
        void AtualizaObraPorVolume(Novel obra, VolumeNovel volume);
        Task<VolumeNovel> RetornaVolumeExistente(int obraId, string numero);
    }
}
