using AutoMapper;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models;
using TsundokuTraducoes.Models;

namespace TsundokuTraducoes.Api.Profiles
{
    public class CapituloProfile : Profile
    {
        public CapituloProfile()
        {
            CreateMap<CapituloDTO, CapituloNovel>();
        }
    }
}
