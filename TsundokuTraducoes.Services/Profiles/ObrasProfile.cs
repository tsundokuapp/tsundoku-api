using AutoMapper;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Services.Profiles
{
    public class ObrasProfile : Profile
    {
        public ObrasProfile()
        {
            CreateMap<RetornoNovel, RetornoAppNovel>();
        }
    }
}
