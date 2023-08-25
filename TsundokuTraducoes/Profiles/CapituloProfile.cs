using AutoMapper;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.DTOs.Admin.Retorno;
using TsundokuTraducoes.Api.Models.Capitulo;

namespace TsundokuTraducoes.Api.Profiles
{
    public class CapituloProfile : Profile
    {
        public CapituloProfile()
        {
            CreateMap<CapituloDTO, CapituloNovel>();
            CreateMap<CapituloDTO, CapituloComic>();
            CreateMap<CapituloNovel, RetornoCapituloNovel>();
            CreateMap<CapituloComic, RetornoCapituloComic>();
        }
    }
}
