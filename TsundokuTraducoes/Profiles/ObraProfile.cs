using AutoMapper;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.DTOs.Admin.Retorno;
using TsundokuTraducoes.Api.Models.Obra;
using TsundokuTraducoes.Api.Models.Recomendacao.Comic;

namespace TsundokuTraducoes.Api.Profiles
{
    public class ObraProfile : Profile
    {
        public ObraProfile()
        {
            CreateMap<ObraDTO, Novel>();
            CreateMap<ObraRecomendadaDTO, ComicRecomendada>();
            CreateMap<ComentarioObraRecomendadaDTO, ComicRecomendada>();
            CreateMap<Novel, RetornoObra>();
        }
    }
}