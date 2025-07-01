using AutoMapper;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Services.Profiles
{
    public class ObrasProfile : Profile
    {
        public ObrasProfile()
        {
            CreateMap<RetornoNovel, RetornoAppNovel>();
            CreateMap<Novel, RetornoNovelsRecentes>();
            CreateMap<Comic, RetornoComicsRecentes>();
        }
    }
}
