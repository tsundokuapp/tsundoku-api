using System.Data;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Data;
using TsundokuTraducoes.Api.Repository.Interfaces;

namespace TsundokuTraducoes.Api.Repository
{
    public class Repository : IRepository
    {
        protected readonly TsundokuContext _context;
        protected readonly IDbConnection _contextDapper;

        public Repository(TsundokuContext context)
        {
            _context = context;
            _contextDapper = new TsundokuContextDapper().RetornaSqlConnetionDapper(Configuration.ConnectionString.Default);
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
