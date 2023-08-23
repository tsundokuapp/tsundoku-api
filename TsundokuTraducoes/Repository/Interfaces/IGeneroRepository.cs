using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin.Retorno;
using TsundokuTraducoes.Api.Models;

namespace TsundokuTraducoes.Api.Repository.Interfaces
{
    public interface IGeneroRepository
    {
        Task AdicionaGeneroObra(GeneroObra generoObra);
        void ExcluiGeneroObra(GeneroObra generoObra);
        Task<List<RetornoGenero>> CarregaListaGeneros(List<GeneroObra> generoObras);
    }
}
