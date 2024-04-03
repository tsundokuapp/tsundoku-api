using AutoMapper;
using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Services.Profiles
{
    public class GeneroProfile : Profile
    {
        public GeneroProfile() 
        {
            CreateMap<Genero, GeneroDTO>();
            CreateMap<GeneroDTO, Genero>();
            CreateMap<Genero, RetornoGenero>();
        }
    }
}