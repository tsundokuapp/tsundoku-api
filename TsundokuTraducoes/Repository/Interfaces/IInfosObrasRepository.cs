using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Public;
using TsundokuTraducoes.Api.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Api.Repository.Interfaces
{
    public interface IInfosObrasRepository
    {
        Task<List<RetornoObra>> ObterListaNovels(string pesquisar, string nacionalidade, string status, string tipo, string genero);
        Task<List<RetornoObra>> ObterListaNovelsRecentes();



        // TODO - Será verificado se vai ser reaproveitado enquanto é trabalhado nos backlogs
        ConteudoCapituloNovelDTO ObterCapituloNovelPorSlug(string slugCapitulo);
        ConteudoCapituloComicDTO ObterCapituloComicPorSlug(string slugCapitulo);
        List<DadosCapitulosDTO> ObterCapitulos();
        List<DadosCapitulosDTO> ObterCapitulos(string pesquisar, bool ehNovel);
        List<DadosCapitulosDTO> ObterCapitulos(string nacionalidade, string status, string tipo, string genero, bool ehNovel);
        ObraDTO ObterObraPorSlug(string slug);
    }
}
