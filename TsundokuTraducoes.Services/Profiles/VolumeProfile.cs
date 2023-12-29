using AutoMapper;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Services.Profiles
{
    internal class VolumeProfile : Profile
    {
        public VolumeProfile()
        {
            CreateMap<VolumeDTO, VolumeNovel>();
            CreateMap<VolumeDTO, VolumeComic>();
            CreateMap<VolumeNovel, RetornoVolume>();
            CreateMap<VolumeComic, RetornoVolume>();
        }
    }
}
