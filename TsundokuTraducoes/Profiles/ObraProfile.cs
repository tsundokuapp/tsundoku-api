using AutoMapper;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models;

namespace TsundokuTraducoes.Api.Profiles
{
    public class ObraProfile : Profile
    {
        public ObraProfile()
        {
            CreateMap<ObraDTO, Obra>();
            CreateMap<ObraRecomendadaDTO, ObraRecomendada>();
            CreateMap<ComentarioObraRecomendadaDTO, ComentarioObraRecomendada>();
        }
    }
}