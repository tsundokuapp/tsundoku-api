using System.Collections.Generic;
using TsundokuTraducoes.Api.DTOs.Public;

namespace TsundokuTraducoes.Api.Repository.Interfaces
{
    public interface IInfosObrasRepository
    {
        List<DadosCapitulosDTO> ObterCapitulos();
        List<DadosCapitulosDTO> ObterCapitulos(string pesquisar, bool ehNovel);
        List<DadosCapitulosDTO> ObterCapitulos(string nacionalidade, string status, string tipo, string genero, bool ehNovel);
        ObraDTO ObterObraPorSlug(string slug);
        ConteudoCapituloNovelDTO ObterCapituloPorSlug(string slug);
    }
}
