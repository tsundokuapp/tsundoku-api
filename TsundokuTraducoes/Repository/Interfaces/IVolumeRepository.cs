using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models;

namespace TsundokuTraducoes.Api.Repository.Interfaces
{
    public interface IVolumeRepository
    {
        void AdicionaVolume(Volume volume);
        void ExcluiVolume(Volume volume);
        bool AlteracoesSalvas();
        Volume AtualizaVolume(VolumeDTO VolumeDTO);
        Task<List<Volume>> RetornaListaVolumes(int? idObra);
        Task<Volume> RetornaVolumePorId(int volumeId);
        void AtualizaObraPorVolume(Obra obra, Volume volume);
        Task<Volume> RetornaVolumeExistente(int obraId, string numero);
    }
}
