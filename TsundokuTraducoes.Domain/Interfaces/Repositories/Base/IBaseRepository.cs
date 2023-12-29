namespace TsundokuTraducoes.Domain.Interfaces.Repositories.Base
{
    public interface IBaseRepository
    {
        Task<bool> AlteracoesSalvas();
        Task AdicionaEntidadeBancoDados<T>(T Entidade);
        void ExcluiEntidadeBancoDados<T>(T Entidade);
    }
}
