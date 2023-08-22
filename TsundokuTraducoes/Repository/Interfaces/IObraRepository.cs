using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models;

namespace TsundokuTraducoes.Api.Repository.Interfaces
{
    public interface IObraRepository
    {
        Task Adiciona<T>(T entity) where T : class;
        void Atualiza<T>(T entity) where T : class;
        void Exclui<T>(T entity) where T : class;
        Task<bool> AlteracoesSalvas();
        Task<Obra> AtualizaObra(ObraDTO obraDTO);
        Task<Obra> RetornaObraPorId(int obraId);
        Task<List<Obra>> RetornaListaObras();
        Task<List<Genero>> RetornaListaGeneros();        
        Task InsereGenerosObra(ObraDTO obraDTO, Obra obra, bool inclusao);
        void InsereListaComentariosObraRecomendada(ObraRecomendadaDTO obraRecomendadaDTO, ObraRecomendada obraRecomendada);
        Task InsereComentarioObraRecomendada(ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO);
        ComentarioObraRecomendada RetornaComentarioObraRecomendadaPorId(int id);
        ComentarioObraRecomendada AtualizaComentarioObraRecomendada(ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO);
        List<ObraRecomendada> RetornaListaObraRecomendada();
        ObraRecomendada RetornaObraRecomendadaPorId(int id);
        ObraRecomendada RetornaObraRecomendadaPorObraId(int idObra);
        Task<Obra> RetornaObraExistente(string titulo);
    }
}
