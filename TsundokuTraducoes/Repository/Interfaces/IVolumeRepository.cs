using System.Collections.Generic;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models;

namespace TsundokuTraducoes.Api.Repository.Interfaces
{
    public interface IVolumeRepository
    {
        void Adiciona<T>(T entity) where T : class;
        void Atualiza<T>(T entity) where T : class;
        void Exclui<T>(T entity) where T : class;
        bool AlteracoesSalvas();
        Volume AtualizaVolume(VolumeDTO VolumeDTO);
        List<Volume> RetornaListaVolumes(int? idObra);
        Volume RetornaVolumePorId(int volumeId);
        Obra RetornaObraPorId(int obraId);
        void AtualizaObraPorVolume(Obra obra, Volume volume);
        Volume RetornaVolumeExistente(int obraId, string numero);
    }
}
