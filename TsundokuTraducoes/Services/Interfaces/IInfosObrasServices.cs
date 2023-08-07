using System.Collections.Generic;
using TsundokuTraducoes.Api.DTOs.Public;

namespace TsundokuTraducoes.Api.Services.Interfaces
{
    public interface IInfosObrasServices
    {
        ConteudoCapituloNovelDTO ObterCapituloPorSlug(string slugCapitulo);
        List<DadosCapitulosDTO> ObterCapitulos();
        List<DadosCapitulosDTO> ObterCapitulos(string pesquisar, string nacionalidade, string status, string tipo, string genero, bool ehNovel);
        ObraDTO ObterObraPorSlug(string slug);
    }
}
