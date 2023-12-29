using System.Data;
using TsundokuTraducoes.Data.Configuration;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Domain.Interfaces.Repositories.Base;

#nullable disable

namespace TsundokuTraducoes.Data.Repositories.Base
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly ContextBase _context;
        protected readonly IDbConnection _contextDapper;

        public BaseRepository(ContextBase context)
        {
            _context = context;
            _contextDapper = new ContextBaseDapper().RetornaSqlConnetionDapper(SourceConnection.RetornaConnectionStringConfig());
        }

        public async Task<bool> AlteracoesSalvas()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task AdicionaEntidadeBancoDados<T>(T Entidade)
        {
            await _context.AddAsync(Entidade);
        }

        public void ExcluiEntidadeBancoDados<T>(T Entidade)
        {
            _context.Remove(Entidade);
        }
    }
}
