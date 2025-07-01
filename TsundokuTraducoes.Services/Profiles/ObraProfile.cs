using AutoMapper;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Services.Profiles
{
    public class ObraProfile : Profile
    {
        public ObraProfile()
        {
            CreateMap<Novel, ObraDTO>();
            CreateMap<ObraDTO, Novel>();
            CreateMap<Comic, ObraDTO>();
            CreateMap<ObraDTO, Comic>();
            CreateMap<Novel, RetornoObra>();
            CreateMap<Comic, RetornoObra>();
        }
    }
}
