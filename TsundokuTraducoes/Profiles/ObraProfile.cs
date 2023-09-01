using AutoMapper;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.DTOs.Admin.Retorno;
using TsundokuTraducoes.Api.Models.Obra;

namespace TsundokuTraducoes.Api.Profiles
{
    public class ObraProfile : Profile
    {
        public ObraProfile()
        {
            CreateMap<ObraDTO, Novel>();
            CreateMap<ObraDTO, Comic>();
            CreateMap<Novel, RetornoObra>();
            CreateMap<Comic, RetornoObra>();
        }
    }
}