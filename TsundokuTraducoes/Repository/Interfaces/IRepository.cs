using System.Threading.Tasks;

namespace TsundokuTraducoes.Api.Repository.Interfaces
{
    public interface IRepository
    {
        Task<bool> AlteracoesSalvas();
        Task AdicionaEntidadeBancoDados<T>(T Entidade);
        void ExcluiEntidadeBancoDados<T>(T Entidade);
    }
}
