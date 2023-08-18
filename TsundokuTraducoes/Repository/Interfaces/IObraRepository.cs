using FluentResults;
using System.Collections.Generic;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models;

namespace TsundokuTraducoes.Api.Repository.Interfaces
{
    public interface IObraRepository
    {
        void Adiciona<T>(T entity) where T : class;
        void Atualiza<T>(T entity) where T : class;
        void Exclui<T>(T entity) where T : class;
        bool AlteracoesSalvas();
        public Obra AtualizaObra(ObraDTO obraDTO);
        Obra RetornaObraPorId(int obraId);
        List<Obra> RetornaListaObras();
        List<Genero> RetornaListaGeneros();        
        void InsereGenerosObra(ObraDTO obraDTO, Obra obra, bool inclusao);
        void InsereListaComentariosObraRecomendada(ObraRecomendadaDTO obraRecomendadaDTO, ObraRecomendada obraRecomendada);
        void InsereComentarioObraRecomendada(ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO);
        ComentarioObraRecomendada RetornaComentarioObraRecomendadaPorId(int id);
        ComentarioObraRecomendada AtualizaComentarioObraRecomendada(ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO);
        List<ObraRecomendada> RetornaListaObraRecomendada();
        ObraRecomendada RetornaObraRecomendadaPorId(int id);
        ObraRecomendada RetornaObraRecomendadaPorObraId(int idObra);
        Obra RetornaObraExistente(string titulo);
    }
}
