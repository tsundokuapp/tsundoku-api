using MySql.Data.MySqlClient;
using System.Data;

namespace TsundokuTraducoes.Api.Data
{   
    public class TsundokuContextDapper 
    {  
        public IDbConnection RetornaSqlConnetionDapper(string stringDeConexao)
        {
            return new MySqlConnection(stringDeConexao);
        }
    }
}
