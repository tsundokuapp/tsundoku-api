using AutoMapper;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models;

namespace TsundokuTraducoes.Api.Profiles
{
    public class CapituloProfile : Profile
    {
        public CapituloProfile()
        {
            CreateMap<CapituloDTO, CapituloNovel>();
            CreateMap<CapituloDTO, CapituloComic>();
        }
    }
}
