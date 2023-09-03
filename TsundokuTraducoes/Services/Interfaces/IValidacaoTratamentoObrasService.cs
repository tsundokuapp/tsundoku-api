namespace TsundokuTraducoes.Api.Services.Interfaces
{
    public interface IValidacaoTratamentoObrasService
    {
        bool ValidaParametrosObra(string pesquisar, string nacionalidade, string status, string tipo, string genero, int? pagina, int? obrasPorPagina);
        bool ValidaParametrosNovel(string pesquisar, string nacionalidade, string status, string tipo, string genero, int? pagina, int? novelsPorPagina);
        int RetornaSkipTratado(int? pagina);
        int RetornaTakeTratado(int? pagina);
    }
}
