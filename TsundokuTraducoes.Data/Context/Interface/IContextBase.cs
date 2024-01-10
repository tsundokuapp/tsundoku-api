using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;

namespace TsundokuTraducoes.Data.Context.Interface
{
    public interface IContextBase
    {
        public IDbConnection Connection { get; }
        DatabaseFacade Database { get; }
    }
}
