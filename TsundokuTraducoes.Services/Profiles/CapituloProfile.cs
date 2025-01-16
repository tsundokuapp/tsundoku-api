using AutoMapper;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Services.Profiles
{
    internal class CapituloProfile : Profile
    {
        public CapituloProfile()
        {
            CreateMap<CapituloDTO, CapituloNovel>();
            CreateMap<CapituloDTO, CapituloComic>();
            CreateMap<CapituloNovel, RetornoCapitulo>();
            CreateMap<CapituloComic, RetornoCapitulo>();
        }
    }
}
