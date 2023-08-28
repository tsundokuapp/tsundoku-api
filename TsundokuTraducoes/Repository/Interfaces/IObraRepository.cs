using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models.Genero;
using TsundokuTraducoes.Api.Models.Obra;
using TsundokuTraducoes.Api.Models.Recomendacao.Comic;

namespace TsundokuTraducoes.Api.Repository.Interfaces
{
    public interface IObraRepository
    {
        Task AdicionaObra(Novel obra);
        Task AdicionaComentarioObraRecomendada(ComentarioComicRecomendada comentarioObraRecomendada);
        Task AdicionaObraRecomendada(ComicRecomendada obraRecomendada);
        void ExcluiObra(Novel obra);
        Task<bool> AlteracoesSalvas();
        Task<Novel> AtualizaObra(ObraDTO obraDTO);
        Task<Novel> RetornaObraPorId(int obraId);
        Task<List<Novel>> RetornaListaObras();
        Task<List<Genero>> RetornaListaGeneros();        
        Task InsereGenerosObra(ObraDTO obraDTO, Novel obra, bool inclusao);
        void InsereListaComentariosObraRecomendada(ObraRecomendadaDTO obraRecomendadaDTO, ComicRecomendada obraRecomendada);
        Task InsereComentarioObraRecomendada(ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO);
        ComentarioComicRecomendada RetornaComentarioObraRecomendadaPorId(int id);
        ComentarioComicRecomendada AtualizaComentarioObraRecomendada(ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO);
        List<ComicRecomendada> RetornaListaObraRecomendada();
        ComicRecomendada RetornaObraRecomendadaPorId(int id);
        ComicRecomendada RetornaObraRecomendadaPorObraId(int idObra);
        Task<Novel> RetornaObraExistente(string titulo);
    }
}
