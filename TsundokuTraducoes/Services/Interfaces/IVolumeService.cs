using FluentResults;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.DTOs.Admin.Retorno;
using TsundokuTraducoes.Api.Models;

namespace TsundokuTraducoes.Api.Services.Interfaces
{
    public interface IVolumeService
    {
        Task<Result<List<RetornoVolume>>> RetornaListaVolume(int? idObra);
        Task<Result<RetornoVolume>> RetornaVolumePorId(int id);
        Task<Result<RetornoVolume>> AdicionaVolume(VolumeDTO volumeDTO);
        Task<Result<RetornoVolume>> AtualizaVolume(VolumeDTO volumeDTO);
        Task<Result<bool>> ExcluiVolume(int idObra);
    }
}
