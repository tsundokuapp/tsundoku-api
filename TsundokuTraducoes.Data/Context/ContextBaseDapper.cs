using MySql.Data.MySqlClient;
using System.Data;

namespace TsundokuTraducoes.Data.Context
{
    public class ContextBaseDapper
    {
        public IDbConnection RetornaSqlConnetionDapper(string stringDeConexao)
        {
            return new MySqlConnection(stringDeConexao);
        }
    }
}
