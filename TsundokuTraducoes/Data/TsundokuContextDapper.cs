using MySql.Data.MySqlClient;
using System.Data;
using TsundokuTraducoes.Api.Utilidades;

namespace TsundokuTraducoes.Api.Data
{
    public class TsundokuContextDapper
    {  
        public IDbConnection RetornaSqlConnetionDapper()
        {
            return new MySqlConnection(Constantes.StringDeConexao);
        }
    }
}
