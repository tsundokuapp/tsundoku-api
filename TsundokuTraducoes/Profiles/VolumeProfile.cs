using AutoMapper;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.DTOs.Admin.Retorno;
using TsundokuTraducoes.Api.Models.Volume;

namespace TsundokuTraducoes.Api.Profiles
{
    public class VolumeProfile : Profile
    {
        public VolumeProfile()
        {
            CreateMap<VolumeDTO, VolumeNovel>();
            CreateMap<VolumeNovel, RetornoVolume>();
        }
    }
}
