using System.Collections.Generic;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models;

namespace TsundokuTraducoes.Api.Repository.Interfaces
{
    public interface ICapituloRepository
    {
        void Adiciona<T>(T entity) where T : class;
        void Atualiza<T>(T entity) where T : class;
        void Exclui<T>(T entity) where T : class;
        bool AlteracoesSalvas();

        List<CapituloNovel> RetornaListaCapitulos();
        CapituloNovel RetornaCapituloPorId(int capituloId);
        Volume RetornaVolumePorId(int volumeId);
        Obra RetornaObraPorId(int obraId);
        void ExcluiTabelaUrlImagensManga(CapituloNovel capitulo);
        CapituloNovel AtualizaCapitulo(CapituloDTO capituloDTO);
        void AtualizaObraPorCapitulo(Obra obra, CapituloNovel capitulo);
    }
}
