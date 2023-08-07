using System.Collections.Generic;
using TsundokuTraducoes.Api.DTOs.Public;

namespace TsundokuTraducoes.Api.Services.Interfaces
{
    public interface IInfosObrasServices
    {
        ConteudoCapituloNovelDTO ObterCapituloNovelPorSlug(string slugCapitulo);
        ConteudoCapituloComicDTO ObterCapituloComicPorSlug(string slugCapitulo);
        List<DadosCapitulosDTO> ObterCapitulos();
        List<DadosCapitulosDTO> ObterCapitulos(string pesquisar, string nacionalidade, string status, string tipo, string genero, bool ehNovel);
        ObraDTO ObterObraPorSlug(string slug);
    }
}
