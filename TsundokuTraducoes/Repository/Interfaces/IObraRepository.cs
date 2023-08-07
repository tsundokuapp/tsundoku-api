using System.Collections.Generic;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models;
using TsundokuTraducoes.Models;

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
        void InsereGenerosObra(ObraDTO obraDTO);
        void CarregaListaGeneros(ObraDTO obraDTO, Obra obra, bool inclusao);
        void CarregaVolumes(List<Volume> volumes, Volume volume);
        void CarregaCapitulos(List<Volume> volumes, CapituloNovel capitulo, CapituloManga urlImagensManga = null);
        void CarregaImagensManga(List<CapituloNovel> capitulos, CapituloManga urlImagensManga = null);
    }
}
