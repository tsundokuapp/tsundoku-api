using FluentResults;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models;

namespace TsundokuTraducoes.Api.Services.Interfaces
{
    public interface IVolumeService
    {
        Result<List<Volume>> RetornaListaVolume(int? idObra);
        Result<Volume> RetornaVolumePorId(int id);
        Task<Result<Volume>> AdicionaVolume(VolumeDTO volumeDTO);
        Task<Result<Volume>> AtualizaVolume(VolumeDTO volumeDTO);
        Result<bool> ExcluirVolume(int idObra);
    }
}
